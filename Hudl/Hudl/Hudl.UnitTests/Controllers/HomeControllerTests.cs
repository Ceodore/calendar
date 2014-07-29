using System;
using Hudl.Services.Calendar;
using Hudl.Services.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Hudl.UnitTests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestClass]
        public class IndexTests
        {
            private Mock<IEventsBuilder> _iEventsBuilderMock;
            private Mock<ICalendarBuilder> _iCalendarBuilderMock;
            
            [TestInitialize]
            public void Initialize()
            {
                _iEventsBuilderMock = new Mock<IEventsBuilder>();
                _iCalendarBuilderMock = new Mock<ICalendarBuilder>();
            }
        
            [TestMethod]
            public void TestMethod1()
            {
            }
        }
    }
}
