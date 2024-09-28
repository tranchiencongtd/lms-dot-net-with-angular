import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// Page Route
import { CoursesLearningWithTeacherRoutingModule } from './learning-with-teacher-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';

// Select Drop down
import { NgSelectModule } from '@ng-select/ng-select';

// Range Slider
import { NgxSliderModule } from 'ngx-slider-v2';

//Wizard
import { CdkStepperModule } from '@angular/cdk/stepper';
import { NgStepperModule } from 'angular-ng-stepper';

// Ck Editor
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';

// Simplebar
import { SimplebarAngularModule } from 'simplebar-angular';

// Flat Picker
import { FlatpickrModule } from 'angularx-flatpickr';

// Drop Zone
import { DropzoneModule } from 'ngx-dropzone-wrapper';
import { DROPZONE_CONFIG } from 'ngx-dropzone-wrapper';
import { DropzoneConfigInterface } from 'ngx-dropzone-wrapper';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { RatingModule } from 'ngx-bootstrap/rating';

// Component
import { ListLearningWithTeacherComponent } from './list-learning-with-teacher/list-learning-with-teacher.component';
import { GridLearningWithTeacherComponent } from './grid-learning-with-teacher/grid-learning-with-teacher.component';
import { CategoryLearningOnlineComponent } from './category-learning-online/category-learning-online.component';
import { OverviewComponent } from './overview/overview.component';
import { CreateLearningOnlineComponent } from './create-learning-online/create-learning-online.component';

const DEFAULT_DROPZONE_CONFIG: DropzoneConfigInterface = {
  // Change this to your upload POST address:
  url: 'https://httpbin.org/post',
  maxFilesize: 50,
  acceptedFiles: 'image/*'
};

@NgModule({
  declarations: [
    ListLearningWithTeacherComponent,
    GridLearningWithTeacherComponent,
    CategoryLearningOnlineComponent,
    OverviewComponent,
    CreateLearningOnlineComponent
  ],
  imports: [
    CommonModule,
    CoursesLearningWithTeacherRoutingModule,
    SharedModule,
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot(),
    ModalModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    AccordionModule.forRoot(),
    TabsModule.forRoot(),
    SimplebarAngularModule,
    CKEditorModule,
    DropzoneModule,
    NgStepperModule,
    CdkStepperModule,
    RatingModule,
    NgSelectModule,
    NgxSliderModule,
    FlatpickrModule.forRoot()
  ],
  providers: [
    {
      provide: DROPZONE_CONFIG,
      useValue: DEFAULT_DROPZONE_CONFIG
    }
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class CoursesLearningWithTeacherModule { }
