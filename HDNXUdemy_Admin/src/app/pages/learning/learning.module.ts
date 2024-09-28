import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// Page Route
import { LearningRoutingModule } from './learning-routing.module';
import { CoursesModule } from './courses/courses.module';
import { StudentModule } from './student/student.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { TeachersModule } from './teacher/teacher.module';
import { CoursesLearningWithTeacherModule } from './learning-with-teacher/learning-with-teacher.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    LearningRoutingModule,
    CoursesModule,
    SharedModule,
    StudentModule,
    TeachersModule,
    CoursesLearningWithTeacherModule,
  ],
  providers: []
})
export class LearningModule { }
