import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Component
import { ListLearningWithTeacherComponent } from './list-learning-with-teacher/list-learning-with-teacher.component';
import { GridLearningWithTeacherComponent } from './grid-learning-with-teacher/grid-learning-with-teacher.component';
import { CategoryLearningOnlineComponent } from './category-learning-online/category-learning-online.component';
import { OverviewComponent } from './overview/overview.component';
import { CreateLearningOnlineComponent } from './create-learning-online/create-learning-online.component';

const routes: Routes = [
  {
    path: "list-course-learning-online",
    component: ListLearningWithTeacherComponent
  },
  {
    path: "grid-course-learning-online",
    component: GridLearningWithTeacherComponent
  },
  {
    path: "category-learning-online",
    component: CategoryLearningOnlineComponent
  },
  {
    path: "overview-learning-online",
    component: OverviewComponent
  },
  {
    path: "create-learning-online",
    component: CreateLearningOnlineComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CoursesLearningWithTeacherRoutingModule { }
