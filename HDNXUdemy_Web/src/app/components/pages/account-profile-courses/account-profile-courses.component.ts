import { Component, OnInit } from '@angular/core';
import { StudentServices } from 'src/app/core/services/student.service';
import { LocalStorageConfig } from 'src/app/library/clientconfig/localstorageconfig';
import { Course } from 'src/app/models/models/course';
import { LoginRegister } from 'src/app/models/respone_model/login-register-respone';

@Component({
  selector: 'app-account-profile-courses',
  templateUrl: './account-profile-courses.component.html',
  styleUrls: ['./account-profile-courses.component.scss']
})
export class AccountProfileCoursesComponent implements OnInit {

  value: string = '100%';
  courses: Course[] = [];
  user: LoginRegister;
  constructor(
    private readonly studentServices: StudentServices,
  ) { }

  ngOnInit() { 
    this.getDataCourseStudent();
    this.getInfomationOfUser();
  }

  getDataCourseStudent() {
    this.studentServices.getDataCourseOfStudent().subscribe((res) => {
      if (res.retCode === 0 && res.systemMessage === '') {
        this.courses = res.data;
      }
    });
  }

  getInfomationOfUser() {
    this.user = LocalStorageConfig.GetUser();
  }

}
