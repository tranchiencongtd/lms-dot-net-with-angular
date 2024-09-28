import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// Page Route
import { CoursesRoutingModule } from './courses-routing.module';
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
import { ListCourseComponent } from './list-courses/list-courses.component';
import { CategoryComponent } from '../../manager-system/category/category.component';
import { OverviewComponent } from './overview/overview.component';
import { CreateCourseComponent } from './create-course/create-course.component';
import { TransferHttp } from 'src/app/core/transfer-http/transfer-http';
import { CreateCourseDetailsComponent } from './create-course-details/create-course-details.component';
import { FormatMoneyDirective } from 'src/app/directives/format-money.directive';
import { NumberFormatPipe } from 'src/app/pipeTransform/format_current_number';
import { EditCourseComponent } from './edit-course/edit-course.component';

const DEFAULT_DROPZONE_CONFIG: DropzoneConfigInterface = {
  url:'',
  maxFilesize: 100,
  maxFiles: 1,
};

@NgModule({
  declarations: [
    ListCourseComponent,
    CategoryComponent,
    OverviewComponent,
    CreateCourseComponent,
    CreateCourseDetailsComponent,
    FormatMoneyDirective,
    NumberFormatPipe,
    EditCourseComponent
  ],
  imports: [
    CommonModule,
    CoursesRoutingModule,
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
    FlatpickrModule.forRoot(),
  ],
  providers: [
    {
      provide: DROPZONE_CONFIG,
      useValue: DEFAULT_DROPZONE_CONFIG
    }, TransferHttp
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class CoursesModule { }
