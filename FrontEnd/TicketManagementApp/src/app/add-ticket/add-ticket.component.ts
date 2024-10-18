import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TicketService } from '../ticket.service';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from '../error-dialog/error-dialog.component';  // Import the ErrorDialogComponent

@Component({
  selector: 'app-add-ticket',
  templateUrl: './add-ticket.component.html',
  styleUrls: ['./add-ticket.component.css']
})
export class AddTicketComponent {
  addTicketForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private ticketService: TicketService,
    private router: Router,
    private dialog: MatDialog  // Inject MatDialog
  ) {
    this.addTicketForm = this.fb.group({
      description: ['', Validators.required],
      status: ['Open', Validators.required],
      date: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.addTicketForm.valid) {
      this.ticketService.createTicket(this.addTicketForm.value).subscribe(
        () => {
          this.router.navigate(['/tickets']);  // Redirect on success
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
