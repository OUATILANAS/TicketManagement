import { Component, OnInit } from '@angular/core';
import { Ticket } from '../ticket.model';
import { TicketService } from '../ticket.service';

@Component({
  selector: 'app-ticket-list',
  templateUrl: './ticket-list.component.html',
  styleUrls: ['./ticket-list.component.css']
})
export class TicketListComponent implements OnInit {
  tickets: Ticket[] = []; // Array to hold the tickets
  paginatedTickets: Ticket[] = []; // Array for the current page of tickets
  filterDescription: string = ''; // Description filter
  filterStatus: string | null = null; // Status filter
  currentPage: number = 1; // Current page number
  itemsPerPage: number = 10; // Items per page
  totalTickets: number = 0; // Total number of tickets
  totalPages: number = 0; // Total number of pages

  constructor(private ticketService: TicketService) {}

  ngOnInit(): void {
    this.loadTickets();
  }

  loadTickets(): void {
    this.ticketService.getTickets().subscribe({
      next: (data) => {
        this.tickets = data;
        this.updatePaginatedTickets(); // Call this to set the initial pagination
      },
      error: (error) => {
        console.error('Error fetching tickets:', error);
      }
    });
  }

  // Update the paginated tickets based on currentPage
  updatePaginatedTickets(): void {
    // Sort and filter tickets as before
    this.sortTickets();
    const filteredTickets = this.tickets.filter(ticket => {
      const matchesDescription = ticket.description.toLowerCase().includes(this.filterDescription.toLowerCase());
      const matchesStatus = this.filterStatus ? ticket.status === this.filterStatus : true;
      return matchesDescription && matchesStatus;
    });

    // Update total tickets and calculate total pages
    this.totalTickets = filteredTickets.length;
    this.totalPages = Math.ceil(this.totalTickets / this.itemsPerPage); // Calculate total pages

    // Implement pagination
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    this.paginatedTickets = filteredTickets.slice(startIndex, startIndex + this.itemsPerPage);
  }

  // Handle page change
  onPageChange(page: number): void {
    if (page >= 1 && page <= this.totalPages) { // Validate page number
      this.currentPage = page;
      this.updatePaginatedTickets(); // Update tickets for the new page
    }
  }

  // Handle previous and next page clicks
  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePaginatedTickets();
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updatePaginatedTickets();
    }
  }

  // Generate an array of page numbers for pagination
  getPageNumbers(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  // Example sort function (update as necessary)
  sortTickets(): void {
    // Add your sorting logic here
    this.tickets.sort((a, b) => a.ticket_ID - b.ticket_ID); // Example: sorting by ID
  }

  deleteTicket(id: number): void {
    if (confirm('Are you sure you want to delete this ticket?')) {
      this.ticketService.deleteTicket(id).subscribe(() => {
        this.loadTickets();  // Refresh the ticket list after deleting
      });
    }
  }
  goToPage(page: number) {
    this.currentPage = page;
    this.updatePaginatedTickets();
  }
}
