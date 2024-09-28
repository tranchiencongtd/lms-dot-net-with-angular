import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// Flat Picker
import { FlatpickrModule } from 'angularx-flatpickr';

// Page Route
import { StudentRoutingModule } from './student-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';

// Bootstrap Component
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';

// Component
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DropzoneModule } from 'ngx-dropzone-wrapper';
import { ListStudentComponent } from './list-student/list-student.component';
import { NgxSliderModule } from 'ngx-slider-v2';
import { AddEditStudentComponent } from './add-edit-student/add-edit-student.component';
import { FormatPhoneDirective } from 'src/app/directives/format-phone.directive';



@NgModule({
  declarations: [
    ListStudentComponent,
    AddEditStudentComponent,
    FormatPhoneDirective
  ],
  imports: [
    CommonModule,
    StudentRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    BsDatepickerModule.forRoot(),
    ProgressbarModule.forRoot(),
    TooltipModule.forRoot(),
    FlatpickrModule.forRoot(),
    PaginationModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    DropzoneModule,
    NgxSliderModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class StudentModule { }
