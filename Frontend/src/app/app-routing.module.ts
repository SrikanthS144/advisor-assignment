import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddAdviseComponent } from './add-advise/add-advise.component';
import { ListContainerComponent } from './list/list-container/list-container.component';

const routes: Routes = [
  { path: 'add-advise', component: AddAdviseComponent},
  { path: '', component: ListContainerComponent},
  { path: 'add-advise/:id', component: AddAdviseComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
