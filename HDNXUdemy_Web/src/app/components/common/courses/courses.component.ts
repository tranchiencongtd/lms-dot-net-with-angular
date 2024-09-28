import { Component, Input, OnInit } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { MessengerServices } from 'src/app/core/services/messenger.service';
import { StudentServices } from 'src/app/core/services/student.service';
import { LocalStorageConfig } from 'src/app/library/clientconfig/localstorageconfig';
import { BookMarkCourse } from 'src/app/models/models/book-mark';
import { ListContentData } from 'src/app/models/respone_model/home-data';

@Component({
    selector: 'app-courses',
    templateUrl: './courses.component.html',
    styleUrls: ['./courses.component.scss'],
    host: { class: 'd-block' },
})
export class CoursesComponent implements OnInit {

    constructor(
        private readonly messengerServices: MessengerServices,
        private studentServices: StudentServices,
    ) { }

    ngOnInit(): void { }
    @Input() listCourse?: ListContentData | undefined | null = null;
    coursesSlides: OwlOptions = {
        loop: true,
        nav: true,
        dots: false,
        autoplayHoverPause: true,
        autoplay: true,
        margin: 30,
        navText: [
            "<i class='flaticon-chevron'></i>",
            "<i class='flaticon-right-arrow'></i>"
        ],
        responsive: {
            0: {
                items: 1
            },
            576: {
                items: 1
            },
            768: {
                items: 2
            },
            992: {
                items: 3
            },
            1080: {
                items: 4
            }
        }
    }

    bookMarkCourse(idCourser: number) {
        const user = LocalStorageConfig.GetUser();
        if (user) {
            var dataInsert: BookMarkCourse = {
                idStudent: user.userId,
                idCourse: idCourser
            }
            this.studentServices.createBookmarkCourse(dataInsert).subscribe((res) => {
                if (res.retCode === 0 && res.systemMessage === '') {
                    this.listCourse.listDataOfContent.length
                    this.listCourse.listDataOfContent.forEach((item) => {
                        if (item.id === idCourser) {
                            item.isBookMark = !item.isBookMark;
                        }
                    })
                }
            });

        } else {
            this.messengerServices.warringBookMarkCourse('Vui lòng đăng nhập để lưu khoá học!');
        }
    }

}