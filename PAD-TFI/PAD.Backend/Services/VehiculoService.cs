using Microsoft.EntityFrameworkCore;
using PAD.Backend.Data;
using PAD.Backend.Dtos;
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
                .Include(v => v.Modelo)
                    .ThenInclude(m => m.Marca)
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

        public async Task<VehiculoResponseDto?> ObtenerVehiculoPorPatenteAsync(string patenteNumero)
        {
            var vehiculoDto = await _context.Patentes
                .AsNoTracking()
                .Include(p => p.Vehiculo)
                    .ThenInclude(v => v.Modelo)
                    .ThenInclude(m => m.Marca)
                //.Include(p => p.Vehiculo)
                //    .ThenInclude(v => v.Modelo) 
                .Where(p => p.NumeroPatente == patenteNumero)
                .Select(p => new VehiculoResponseDto 
                {
                    Marca = p.Vehiculo.Modelo.Marca.Nombre,
                    Modelo = p.Vehiculo.Modelo.Nombre,
                    Categoria = p.Vehiculo.Categoria.ToString(),
                    Precio = p.Vehiculo.Precio,
                    FechaFabricacion = p.Vehiculo.FechaFabricacion,
                    NumeroChasis = p.Vehiculo.NumeroChasis,
                    NumeroMotor = p.Vehiculo.NumeroMotor
                })
                .FirstOrDefaultAsync();

            return vehiculoDto;
        }

        public async Task<List<VehiculoResponseDto>> ObtenerVehiculosPorCuilAsync(string cuil)
        {
            return await _context.Titulares
               .AsNoTracking()
               .Where(t => t.Cuil == cuil)
               .SelectMany(t => t.Patentes)
               .Select(p => new VehiculoResponseDto
               {
                   Marca = p.Vehiculo.Modelo.Marca.Nombre,
                   Modelo = p.Vehiculo.Modelo.Nombre,
                   Categoria = p.Vehiculo.Categoria.ToString(),
                   Precio = p.Vehiculo.Precio,
                   FechaFabricacion = p.Vehiculo.FechaFabricacion,
                   NumeroChasis = p.Vehiculo.NumeroChasis,
                   NumeroMotor = p.Vehiculo.NumeroMotor
               })
               .ToListAsync();
                }
    }
}
