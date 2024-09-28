import { Component, Input, OnInit, ViewChild, inject } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxStarsComponent } from 'ngx-stars';
import { CourseServices } from 'src/app/core/services/course.service';
import { MessengerServices } from 'src/app/core/services/messenger.service';
import { ConstantValue } from 'src/app/models/contants/ennum_router';
import { CourseEvaluation } from 'src/app/models/models/course-evaluation';

@Component({
  selector: 'app-user-review-courses',
  templateUrl: './user-review-courses.component.html',
  styleUrls: ['./user-review-courses.component.scss']
})
export class UserReviewCoursesComponent implements OnInit {
  activeModal = inject(NgbActiveModal);
  @ViewChild(NgxStarsComponent)
  starsComponent: NgxStarsComponent;

  ratingDisplay: number;
  @Input() reviewOfUser: string;
  @Input() idCourse: number;
  @Input() idStudent: number;
  constructor(
    private readonly courserServices: CourseServices,
    private readonly messengerServices: MessengerServices,
  ) { }

  ngOnInit() {}

  onRatingSet(rating: number): void {
    this.ratingDisplay = rating;
  }

  saveComment() {
    let dataInsert: CourseEvaluation = {
      idStudent: this.idStudent,
      idCourse: this.idCourse,
      voteStartNumber: Math.round(this.ratingDisplay),
      commentEvaluation: this.reviewOfUser,
    }
    this.courserServices.addCommentOfStudentForCourse(dataInsert).subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.activeModal.close(ConstantValue.finishModal);
      } else {
        this.activeModal.close(ConstantValue.errorModal);
      }
    });
  }

}
