import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TicketListComponent } from './ticket-list/ticket-list.component';
import { EditTicketComponent } from './edit-ticket/edit-ticket.component';
import { AddTicketComponent } from './add-ticket/add-ticket.component';

const routes: Routes = [
  { path: 'edit/:id', component: EditTicketComponent }, 
  { path: 'add', component: AddTicketComponent }, 
  { path: '', component: TicketListComponent },  
  { path: '**', redirectTo: '', pathMatch: 'full' }  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

