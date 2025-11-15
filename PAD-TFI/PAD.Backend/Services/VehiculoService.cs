using Microsoft.EntityFrameworkCore;
using PAD.Backend.Data;
using PAD.Backend.Models.Entidades;
using PAD.Backend.Models.Enums;

namespace PAD.Backend.Services
{
    public class VehiculoService
    {
        private readonly ApplicationDbContext _context;

        public VehiculoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Vehiculo?> ObtenerVehiculoPorIdAsync(int vehiculoId)
        {

            return await _context.Vehiculos
                .Include(v => v.Marca)
                .Include(v => v.Modelo)
                .FirstOrDefaultAsync(v => v.Id == vehiculoId);
        }

        public async Task<int> ObtenerMarcaIdPorNombreAsync(string nombreMarca)
        {
            var marca = await _context.Marcas
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Nombre == nombreMarca);

            if (marca == null)
            {
                throw new InvalidOperationException($"La marca '{nombreMarca}' no está registrada en la base de datos.");
            }
            return marca.Id;
        }
        public async Task<int> ObtenerModeloIdPorNombreAsync(string nombreModelo)
        {
            var modelo = await _context.Modelos
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Nombre == nombreModelo);

            if (modelo == null)
            {
                throw new InvalidOperationException($"El modelo '{nombreModelo}' no está registrado en la base de datos.");
            }
            return modelo.Id;
        }

        public Vehiculo CrearVehiculo(
            int marcaId, int modeloId, CategoriaVehiculo categoria,
            decimal precio, DateOnly fechaFabricacion,
            string numeroChasis, string numeroMotor)
        {
            var vehiculo = new Vehiculo
            {
                MarcaId = marcaId,
                ModeloId = modeloId,
                Categoria = categoria,
                Precio = precio,
                FechaFabricacion = fechaFabricacion,
                NumeroChasis = numeroChasis,
                NumeroMotor = numeroMotor
            };

            _context.Vehiculos.Add(vehiculo);
            return vehiculo;
        }
    

    public async Task ValidarUnicidadVehiculoAsync(string numeroChasis, string numeroMotor)
        {
            bool chasisExiste = await _context.Vehiculos
                .AnyAsync(v => v.NumeroChasis == numeroChasis);

            if (chasisExiste)
            {
                throw new InvalidOperationException($"El Número de Chasis '{numeroChasis}' ya se encuentra registrado.");
            }

            bool motorExiste = await _context.Vehiculos
                .AnyAsync(v => v.NumeroMotor == numeroMotor);

            if (motorExiste)
            {
                throw new InvalidOperationException($"El Número de Motor '{numeroMotor}' ya se encuentra registrado.");
            }
        }
    }
}