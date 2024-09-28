import { Component, OnInit } from '@angular/core';
import { CartServices } from 'src/app/core/services/cart.service';
import { CourseServices } from 'src/app/core/services/course.service';
import { StudentServices } from 'src/app/core/services/student.service';
import { LocalStorageConfig } from 'src/app/library/clientconfig/localstorageconfig';
import { Course } from 'src/app/models/models/course';
import { LoginRegister } from 'src/app/models/respone_model/login-register-respone';

@Component({
  selector: 'app-account-bookmark-course',
  templateUrl: './account-bookmark-course.component.html',
  styleUrls: ['./account-bookmark-course.component.scss']
})
export class AccountBookmarkCourseComponent implements OnInit {
  value: string = '100%';
  course: Course[] = [];
  user: LoginRegister;
  constructor(
    private readonly studentServices: StudentServices,
    private readonly cartServices: CartServices,
    private readonly courseServices: CourseServices,
  ) { }

  ngOnInit() {

    this.loadListBookmarkCourseOfUser();
    this.getInfomationOfUser();
  }

  loadListBookmarkCourseOfUser() {
    this.studentServices.getListBookmarkCourse().subscribe((res) => {
      if (res.retCode === 0 && res.systemMessage === '') {
        this.course = res.data;
      } else {
        this.course = [];
      }
    })
  }

  getInfomationOfUser() {
    this.user = LocalStorageConfig.GetUser();
  }

  addCourseToCart(idCourse : number){
    this.courseServices.getCourses(idCourse).subscribe((res) => {
      if (res.retCode === 0 && res.systemMessage === '') {
        this.cartServices.addCourse(res.data);
      } 
  });
}
}
