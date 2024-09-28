import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Component
import { ListStudentComponent } from './list-student/list-student.component';
import { AddEditStudentComponent } from './add-edit-student/add-edit-student.component';

const routes: Routes = [
  {
    path: 'list-student',
    component: ListStudentComponent,
  },
  {
    path: 'add-edit-student/:id',
    component: AddEditStudentComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StudentRoutingModule { }
