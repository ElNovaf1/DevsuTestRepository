using AutoMapper;
using AutoMapper.Execution;
using Castle.Core.Logging;
using Devsu.API.Profiles;
using Devsu.Domain.Entities;
using Devsu.Domain.Interfaces;
using Devsu.Domain.Interfaces.Services;
using Devsu.Service;
using Devsu.Transversal.DTO.Input;
using Devsu.Transversal.DTO.Output;
using Moq;
using NUnit.Framework.Constraints;

namespace Devsu.UnitTest
{

    public class ClienteServiceTest
    {
        IMapper _mapper;
        MapperConfiguration _config;
        [SetUp]
        public void Setup()
        {
            _config = new MapperConfiguration(cfg => cfg.AddProfile<ClienteProfile>());
            _mapper = _config.CreateMapper();
        }

        [Test]
        public async Task CrearCliente_Exito()
        {
            var mockInputCliente = new Cliente()
            {
                Id = 0,
                Nombre = "Usuario Prueba",
                Contraseña = "123456",
                Edad = 19,
                Genero = "Masculino",
                Telefono = "78996",
                Direccion = "Ciudad Doral",
                Estado = true,
                Cuentas = new List<Cuenta>(),
            };

            var mockOutputCliente = new Cliente()
            {
                Id = 1,
                Nombre = "Usuario Prueba",
                Contraseña = "123456",
                Edad = 19,
                Genero = "Masculino",
                Telefono = "78996",
                Direccion = "Ciudad Doral",
                Estado = true,
                Cuentas = new List<Cuenta>(),
            };

            //CREANDO MOCKS DE REPOSITORIOS Y UNIDAD DE TRABAJO
            var RepositoryMock = new Mock<IRepository<Cliente>>();
            RepositoryMock.Setup(m => m.AddAsync(mockInputCliente)).ReturnsAsync(mockOutputCliente);
            RepositoryMock.Setup(m => m.GetByFilterAsync(x => x.Nombre == mockInputCliente.Nombre)).ReturnsAsync(new List<Cliente>());

            var unitOfWorkMock = new Mock<IWorkUnit>();
            unitOfWorkMock.Setup(m => m.Repository<Cliente>()).Returns(RepositoryMock.Object);
            unitOfWorkMock.Setup(m => m.Begin()).Returns(Task.CompletedTask);
            unitOfWorkMock.Setup(m => m.Commit()).Returns(Task.CompletedTask);
            unitOfWorkMock.Setup(m => m.Rollback()).Returns(Task.CompletedTask);

            IClientService ServiceMock = new ClientService(unitOfWorkMock.Object, _mapper);

            //TESTEANDO
            var testAdd = await ServiceMock.AddAsync(mockInputCliente);
            Assert.IsNotNull(testAdd);
            Assert.IsTrue(testAdd.Id > 0);
        }
        [Test]
        public async Task ObtenerCliente_Exito()
        {
            int IdCliente = 1;
            var mockOutputCliente = new Cliente()
            {
                Id = IdCliente,
                Nombre = "Usuario Prueba",
                Contraseña = "123456",
                Edad = 19,
                Genero = "Masculino",
                Telefono = "78996",
                Direccion = "Ciudad Doral",
                Estado = true,
                Cuentas = new List<Cuenta>(),
            };

            //CREANDO MOCKS DE REPOSITORIOS Y UNIDAD DE TRABAJO
            var RepositoryMock = new Mock<IRepository<Cliente>>();
            RepositoryMock.Setup(m => m.GetByIdAsync(IdCliente)).ReturnsAsync(mockOutputCliente);

            var unitOfWorkMock = new Mock<IWorkUnit>();
            unitOfWorkMock.Setup(m => m.Repository<Cliente>()).Returns(RepositoryMock.Object);
            unitOfWorkMock.Setup(m => m.Begin()).Returns(Task.CompletedTask);
            unitOfWorkMock.Setup(m => m.Commit()).Returns(Task.CompletedTask);
            unitOfWorkMock.Setup(m => m.Rollback()).Returns(Task.CompletedTask);

            IClientService ServiceMock = new ClientService(unitOfWorkMock.Object, _mapper);

            //TESTEANDO
            var testAdd = await ServiceMock.GetByIdAsync(IdCliente);
            Assert.IsNotNull(testAdd);
            Assert.IsTrue(testAdd.Id > 0);
        }


        [Test]
        public async Task EliminarCliente_Exito()
        {
            int IdCliente = 1;
            var mockOutputCliente = new Cliente()
            {
                Id = IdCliente,
                Nombre = "Usuario Prueba",
                Contraseña = "123456",
                Edad = 19,
                Genero = "Masculino",
                Telefono = "78996",
                Direccion = "Ciudad Doral",
                Estado = true,
                Cuentas = new List<Cuenta>(),
            };

            //CREANDO MOCKS DE REPOSITORIOS Y UNIDAD DE TRABAJO
            var RepositoryMock = new Mock<IRepository<Cliente>>();
            RepositoryMock.Setup(m => m.GetByIdAsync(IdCliente)).ReturnsAsync(mockOutputCliente);
            RepositoryMock.Setup(m => m.DeleteAsync(mockOutputCliente.Id)).Returns(Task.CompletedTask);

            var unitOfWorkMock = new Mock<IWorkUnit>();
            unitOfWorkMock.Setup(m => m.Repository<Cliente>()).Returns(RepositoryMock.Object);
            unitOfWorkMock.Setup(m => m.Begin()).Returns(Task.CompletedTask);
            unitOfWorkMock.Setup(m => m.Commit()).Returns(Task.CompletedTask);
            unitOfWorkMock.Setup(m => m.Rollback()).Returns(Task.CompletedTask);

            IClientService ServiceMock = new ClientService(unitOfWorkMock.Object, _mapper);

            //TESTEANDO
            var testAdd = await ServiceMock.DeleteAsync(IdCliente);
            Assert.IsNotNull(testAdd);
            Assert.IsTrue(testAdd.Id > 0);
        }

        [Test]
        public async Task ActualizarCliente_Exito()
        {
            var mockInputCliente = new Cliente()
            {
                Id = 1,
                Nombre = "Usuario Prueba",
                Contraseña = "123456",
                Edad = 19,
                Genero = "Masculino",
                Telefono = "78996",
                Direccion = "Ciudad Doral",
                Estado = true,
                Cuentas = new List<Cuenta>(),
            };

            var mockCurrentCliente = new Cliente()
            {
                Id = 1,
                Nombre = "Usuario Prueba",
                Contraseña = "123456",
                Edad = 25,
                Genero = "Masculino",
                Telefono = "78996",
                Direccion = "Managua",
                Estado = true,
                Cuentas = new List<Cuenta>(),
            };

            var mockOutputCliente = new Cliente()
            {
                Id = 1,
                Nombre = "Usuario Prueba",
                Contraseña = "123456",
                Edad = 19,
                Genero = "Masculino",
                Telefono = "78996",
                Direccion = "Ciudad Doral",
                Estado = true,
                Cuentas = new List<Cuenta>(),
            };

            //CREANDO MOCKS DE REPOSITORIOS Y UNIDAD DE TRABAJO
            var RepositoryMock = new Mock<IRepository<Cliente>>();
            RepositoryMock.Setup(m => m.GetByIdAsync(mockInputCliente.Id)).ReturnsAsync(mockCurrentCliente);
            RepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<Cliente>())).ReturnsAsync(mockOutputCliente);

            var unitOfWorkMock = new Mock<IWorkUnit>();
            unitOfWorkMock.Setup(m => m.Repository<Cliente>()).Returns(RepositoryMock.Object);
            unitOfWorkMock.Setup(m => m.Begin()).Returns(Task.CompletedTask);
            unitOfWorkMock.Setup(m => m.Commit()).Returns(Task.CompletedTask);
            unitOfWorkMock.Setup(m => m.Rollback()).Returns(Task.CompletedTask);

            IClientService ServiceMock = new ClientService(unitOfWorkMock.Object, _mapper);

            //TESTEANDO
            var testAdd = await ServiceMock.UpdateAsync(mockInputCliente.Id, mockInputCliente);
            Assert.IsNotNull(testAdd);
            Assert.IsTrue(testAdd.Id > 0);
            Assert.IsTrue(testAdd.Direccion == mockCurrentCliente.Direccion);
        }
        [Test]
        public async Task ActualizarCliente_Fracaso_NoExiste()
        {
            var mockInputCliente = new Cliente()
            {
                Id = 1,
                Nombre = "Usuario Prueba",
                Contraseña = "123456",
                Edad = 19,
                Genero = "Masculino",
                Telefono = "78996",
                Direccion = "Ciudad Doral",
                Estado = true,
                Cuentas = new List<Cuenta>(),
            };

            var mockCurrentCliente = new Cliente()
            {
                Id = 1,
                Nombre = "Usuario Prueba",
                Contraseña = "123456",
                Edad = 25,
                Genero = "Masculino",
                Telefono = "78996",
                Direccion = "Managua",
                Estado = true,
                Cuentas = new List<Cuenta>(),
            };

            var mockOutputCliente = new Cliente()
            {
                Id = 1,
                Nombre = "Usuario Prueba",
                Contraseña = "123456",
                Edad = 19,
                Genero = "Masculino",
                Telefono = "78996",
                Direccion = "Ciudad Doral",
                Estado = true,
                Cuentas = new List<Cuenta>(),
            };

            //CREANDO MOCKS DE REPOSITORIOS Y UNIDAD DE TRABAJO
            var RepositoryMock = new Mock<IRepository<Cliente>>();
            RepositoryMock.Setup(m => m.GetByIdAsync(mockInputCliente.Id)).ReturnsAsync((Cliente)null);
            RepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<Cliente>())).ReturnsAsync(mockOutputCliente);
            //Task.FromResult<Cliente>(null)

            var unitOfWorkMock = new Mock<IWorkUnit>();
            unitOfWorkMock.Setup(m => m.Repository<Cliente>()).Returns(RepositoryMock.Object);
            unitOfWorkMock.Setup(m => m.Begin()).Returns(Task.CompletedTask);
            unitOfWorkMock.Setup(m => m.Commit()).Returns(Task.CompletedTask);
            unitOfWorkMock.Setup(m => m.Rollback()).Returns(Task.CompletedTask);

            IClientService ServiceMock = new ClientService(unitOfWorkMock.Object, _mapper);

            //TESTEANDO
            var testAdd = await ServiceMock.UpdateAsync(mockInputCliente.Id, mockInputCliente);
            Assert.IsNull(testAdd);
        }
    }
}