using System;
using System.Data;
using System.Data.SqlClient;
using Hudl.Services;
using Hudl.Services.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Hudl.UnitTests.Services.Events
{
    [TestClass]
    public class EventsDataProviderTests
    {
        [TestClass]
        public class GeteventsTests
        {
            private Mock<IDbConnection> _dbConnectionMock;
            private Mock<IConnectionFactory> _connectionFactoryMock;
            private Mock<IDbCommand> _commandMock;
            private Mock<IDbDataParameter> _parameterMock;
            private Mock<IDataParameterCollection> _dataParameterCollectionMock;

            [TestInitialize]
            public void Initialize()
            {
                _connectionFactoryMock = new Mock<IConnectionFactory>();
                _dbConnectionMock = new Mock<IDbConnection>();
                _commandMock = new Mock<IDbCommand>();
                _parameterMock = new Mock<IDbDataParameter>();
                _dataParameterCollectionMock = new Mock<IDataParameterCollection>();
            }

            [TestMethod]
            [ExpectedException(typeof (Exception))]
            public void GetEventsTest_GetConnectionException()
            {
                _connectionFactoryMock.Setup(x => x.GetConnection()).Throws(new Exception());

                var eventsDataProvider = new EventsDataProvider(_connectionFactoryMock.Object);
                eventsDataProvider.Getevents();
            }

            [TestMethod]
            [ExpectedException(typeof (InvalidOperationException))]
            public void GetEventsTest_OpenException()
            {
                var connection = new SqlConnection();
                _connectionFactoryMock.Setup(x => x.GetConnection()).Returns(connection);
                _dbConnectionMock.Setup(x => x.Open()).Throws(new Exception());

                var eventsDataProvider = new EventsDataProvider(_connectionFactoryMock.Object);
                eventsDataProvider.Getevents();
            }

            [TestMethod]
            [ExpectedException(typeof (Exception))]
            public void GetEventsTest_CreateCommandFailed()
            {
                _connectionFactoryMock.Setup(x => x.GetConnection()).Returns(_dbConnectionMock.Object);
                _dbConnectionMock.Setup(x => x.Open());
                _dbConnectionMock.Setup(x => x.CreateCommand()).Throws(new Exception());

                var eventsDataProvider = new EventsDataProvider(_connectionFactoryMock.Object);
                eventsDataProvider.Getevents();
            }
        }

        [TestClass]
        public class AddeventsTests
        {
            private Mock<IDbConnection> _dbConnectionMock;
            private Mock<IConnectionFactory> _connectionFactoryMock;

            [TestInitialize]
            public void Initialize()
            {
                _connectionFactoryMock = new Mock<IConnectionFactory>();
                _dbConnectionMock = new Mock<IDbConnection>();
            }

            [TestMethod]
            public void AddeventsTest_Valid()
            {
            }
        }
    }
}