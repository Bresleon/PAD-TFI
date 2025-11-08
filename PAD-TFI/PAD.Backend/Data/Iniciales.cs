using PAD.Backend.Models.Entidades;
using PAD.Backend.Models.Enums;

namespace PAD.Backend.Data;

public class Iniciales
{
    private static Iniciales? _instance = null;
    private static readonly object _lock = new();

    private Iniciales()
    {
        Inicializar();
    }

    public static Iniciales Instance
    {
        get
        {
            if (_instance is null)
            {
                lock (_lock)
                {
                    _instance = new();
                }
            }

            return _instance;
        }
    }

    public List<Marca> Marcas { get; private set; } = new();
    public List<Modelo> Modelos { get; private set; } = new();
    public List<Patente> Patentes { get; private set; } = new();
    public List<Titular> Titulares { get; private set; } = new();
    public List<Transaccion> Transacciones { get; private set; } = new();
    public List<Vehiculo> Vehiculos { get; private set; } = new();


    private void Inicializar()
    {
        InicializarMarcas();
        InicializarModelos();
        InicializarTitulares();
        InicializarVehiculos();
        InicializarPatentes();
        InicializarTransacciones();
    }

    private void InicializarMarcas()
    {
        Marcas = new List<Marca>
        {
            new Marca { Id = 1, Nombre = "Toyota" },
            new Marca { Id = 2, Nombre = "Ford" },
            new Marca { Id = 3, Nombre = "Chevrolet" },
            new Marca { Id = 4, Nombre = "Volkswagen" }
        };
    }

    private void InicializarModelos()
    {
        Modelos = new List<Modelo>
        {
            new Modelo { Id = 1, Nombre = "Corolla" },
            new Modelo { Id = 2, Nombre = "Hilux" },
            new Modelo { Id = 3, Nombre = "Focus" },
            new Modelo { Id = 4, Nombre = "Onix" },
            new Modelo { Id = 5, Nombre = "Gol" }
        };
    }

    private void InicializarTitulares()
    {
        Titulares = new List<Titular>
        {
            new Titular
            {
                Id = 1,
                Nombre = "Juan",
                Apellido = "Pérez",
                Dni = "30123456",
                Email = "juan.perez@email.com",
                Telefono = "1123456789"
            },
            new Titular
            {
                Id = 2,
                Nombre = "María",
                Apellido = "Gómez",
                Dni = "28999888",
                Email = "maria.gomez@email.com",
                Telefono = "1133344455"
            },
            new Titular
            {
                Id = 3,
                Nombre = "Carlos",
                Apellido = "Ramírez",
                Dni = "27111222",
                Email = "carlos.ramirez@email.com",
                Telefono = "1145566778"
            }
        };
    }

    private void InicializarVehiculos()
    {
        Vehiculos = new List<Vehiculo>
        {
            new Vehiculo
            {
                Id = 1,
                MarcaId = 1, // Toyota
                ModeloId = 1, // Corolla
                Precio = 15000,
                FechaFabricacion = new DateOnly(2018, 5, 10),
                NumeroChasis = "CHS-TOY-001",
                NumeroMotor = "MOT-TOY-001",
                Categoria = CategoriaVehiculo.B
            },
            new Vehiculo
            {
                Id = 2,
                MarcaId = 2, // Ford
                ModeloId = 3, // Focus
                Precio = 13000,
                FechaFabricacion = new DateOnly(2019, 8, 15),
                NumeroChasis = "CHS-FRD-002",
                NumeroMotor = "MOT-FRD-002",
                Categoria = CategoriaVehiculo.B
            },
            new Vehiculo
            {
                Id = 3,
                MarcaId = 3, // Chevrolet
                ModeloId = 4, // Onix
                Precio = 11000,
                FechaFabricacion = new DateOnly(2020, 3, 20),
                NumeroChasis = "CHS-CHV-003",
                NumeroMotor = "MOT-CHV-003",
                Categoria = CategoriaVehiculo.B
            }
        };
    }

    private void InicializarPatentes()
    {
        Patentes = new List<Patente>
        {
            new Patente
            {
                Id = 1,
                VehiculoId = 1,
                TitularId = 1,
                NumeroPatente = "AB123CD",
                Ejemplar = EjemplarPatente.A,
                FechaEmision = new DateOnly(2018, 6, 1)
            },
            new Patente
            {
                Id = 2,
                VehiculoId = 2,
                TitularId = 2,
                NumeroPatente = "AC987XY",
                Ejemplar = EjemplarPatente.B,
                FechaEmision = new DateOnly(2019, 9, 1)
            },
            new Patente
            {
                Id = 3,
                VehiculoId = 3,
                TitularId = 3,
                NumeroPatente = "AE555GH",
                Ejemplar = EjemplarPatente.A,
                FechaEmision = new DateOnly(2020, 4, 10)
            }
        };
    }

    private void InicializarTransacciones()
    {
        Transacciones = new List<Transaccion>
        {
            new Transaccion
            {
                Id = 1,
                TitularOrigenId = 1,
                TitularDestinoId = 2,
                Costo = 12500m,
                TipoTransaccion = TipoTransaccion.TRANSFERENCIA
            },
            new Transaccion
            {
                Id = 2,
                TitularOrigenId = 2,
                TitularDestinoId = 3,
                Costo = 11000m,
                TipoTransaccion = TipoTransaccion.TRANSFERENCIA
            }
        };
    }
}
