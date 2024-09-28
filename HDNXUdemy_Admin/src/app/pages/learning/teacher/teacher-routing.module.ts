import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Component
import { ListTeacherComponent } from './list-teacher/list-teacher.component';
import { GridTeacherComponent } from './grid-teacher/grid-teacher.component';
import { OverviewTeacherComponent } from './overview-teacher/overview-teacher.component';
import { CreateComponent } from './create/create.component';


const routes: Routes = [
  {
    path: "list-teacher",
    component: ListTeacherComponent
  },
  {
    path: "grid-teacher",
    component: GridTeacherComponent
  },
  {
    path: "teacher-overview",
    component: OverviewTeacherComponent
  },
  {
    path: "teacher-create",
    component: CreateComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TeachersRoutingModule { }
