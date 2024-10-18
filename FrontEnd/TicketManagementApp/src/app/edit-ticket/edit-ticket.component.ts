import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TicketService } from '../ticket.service';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from '../error-dialog/error-dialog.component'; // Import the ErrorDialogComponent

@Component({
  selector: 'app-edit-ticket',
  templateUrl: './edit-ticket.component.html',
  styleUrls: ['./edit-ticket.component.css']
})
export class EditTicketComponent implements OnInit {
  editTicketForm: FormGroup;
  ticketId: number;

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private ticketService: TicketService,
    private router: Router,
    private dialog: MatDialog  // Inject MatDialog
  ) {
    this.ticketId = this.route.snapshot.params['id'];  // Get the ticket ID from the route

    this.editTicketForm = this.fb.group({
      description: ['', Validators.required],
      status: ['', Validators.required],
      date: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.ticketService.getTicket(this.ticketId).subscribe((ticket) => {
      // Convert the date to YYYY-MM-DD format
      const formattedDate = this.formatDate(ticket.date);

      this.editTicketForm.patchValue({
        description: ticket.description,
        status: ticket.status,
        date: formattedDate   // Assign formatted date as a string
      });
    });
  }

  // Helper method to format the date from Date or string to YYYY-MM-DD
  private formatDate(date: Date | string): string {
    const d = typeof date === 'string' ? new Date(date) : date; // Ensure date is a Date object
    const year = d.getFullYear();
    const month = ('0' + (d.getMonth() + 1)).slice(-2);  // Ensure 2 digits
    const day = ('0' + d.getDate()).slice(-2);  // Ensure 2 digits
    return `${year}-${month}-${day}`;  // Return formatted date
  }

  onSubmit() {
    if (this.editTicketForm.valid) {
      this.ticketService.updateTicket(this.ticketId, this.editTicketForm.value).subscribe(
        () => {
          this.router.navigate(['/tickets']);  // Redirect to ticket list after editing
        },
        (error) => {
          if (error.status === 400 && error.error.errors) {
            const serverErrors = error.error.errors;

            // Extract the validation messages
            const validationMessages = [];
            for (let field in serverErrors) {
              validationMessages.push(...serverErrors[field]);
            }

            // Open the error dialog
            this.dialog.open(ErrorDialogComponent, {
              data: { errors: validationMessages },
              width: '300px',  
              height: 'auto'   
          });          
          } else {
            console.error('An unexpected error occurred:', error);
          }
        }
      );
    }
  }
}
