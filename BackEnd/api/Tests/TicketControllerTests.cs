using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using api.Controllers;
using api.Services;
using api.DTOs;

namespace api.Tests
{
    public class TicketControllerTests
    {
        private readonly TicketController _controller;
        private readonly Mock<ITicketService> _mockTicketService;

        public TicketControllerTests()
        {
            _mockTicketService = new Mock<ITicketService>();
            _controller = new TicketController(_mockTicketService.Object);
        }

        [Fact]
        public async Task GetTickets_ReturnsOkResult_WithTickets()
        {
            // Arrange
            var tickets = new List<TicketDto>
            {
                new TicketDto { Ticket_ID = 1, Description = "Test Ticket 1", Status = "Open", Date = DateTime.Now },
                new TicketDto { Ticket_ID = 2, Description = "Test Ticket 2", Status = "Closed", Date = DateTime.Now }
            };
            _mockTicketService.Setup(service => service.GetTicketsAsync()).ReturnsAsync(tickets);

            // Act
            var result = await _controller.GetTickets();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnTickets = Assert.IsAssignableFrom<IEnumerable<TicketDto>>(okResult.Value);
            Assert.Equal(2, returnTickets.Count());
        }

        [Fact]
        public async Task GetTicket_ReturnsNotFound_WhenTicketDoesNotExist()
        {
            // Arrange
            int ticketId = 1;
            _mockTicketService.Setup(service => service.GetTicketByIdAsync(ticketId)).ReturnsAsync((TicketDto)null);

            // Act
            var result = await _controller.GetTicket(ticketId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostTicket_ReturnsCreatedAtAction_WhenTicketIsValid()
        {
            // Arrange
            var newTicket = new TicketDto { Ticket_ID = 1, Description = "New Ticket", Status = "Open", Date = DateTime.Now };

            // Act
            var result = await _controller.PostTicket(newTicket);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetTicket", createdAtActionResult.ActionName);
            Assert.Equal(1, createdAtActionResult.RouteValues["id"]);
        }
    }
}
