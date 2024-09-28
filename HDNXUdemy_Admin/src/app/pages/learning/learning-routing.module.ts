import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'courses',
    loadChildren: () =>
      import('./courses/courses.module').then((m) => m.CoursesModule),
  },
  {
    path: 'student',
    loadChildren: () =>
      import('./student/student.module').then((m) => m.StudentModule),
  },
  {
    path: 'teacher',
    loadChildren: () =>
      import('./teacher/teacher.module').then(
        (m) => m.TeachersModule
      ),
  },
  {
    path: 'learning-online-teacher',
    loadChildren: () =>
      import('./learning-with-teacher/learning-with-teacher.module').then(
        (m) => m.CoursesLearningWithTeacherModule
      ),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LearningRoutingModule {}
