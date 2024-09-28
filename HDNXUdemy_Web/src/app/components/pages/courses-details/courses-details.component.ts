import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import lgVideo from 'lightgallery/plugins/video';
import { BeforeSlideDetail } from 'lightgallery/lg-events';
import { ActivatedRoute } from '@angular/router';
import { CourseServices } from 'src/app/core/services/course.service';
import { GetCourseWithDetailsContent } from 'src/app/models/respone_model/course-content-with-detail';
import { LightGallery } from 'lightgallery/lightgallery';
import { CartServices } from 'src/app/core/services/cart.service';
import { AuthenticationServices } from 'src/app/core/services/authentication.service';

@Component({
    selector: 'app-courses-details',
    templateUrl: './courses-details.component.html',
    styleUrls: ['./courses-details.component.scss']
})
export class CoursesDetailsComponent implements OnInit {

    constructor(
        private readonly router: ActivatedRoute,
        private readonly courseServices: CourseServices,
        private readonly cartServices : CartServices,
        private readonly authenticationServices : AuthenticationServices,
    ) { }
    idCourse: number = 0;
    courseContentCourse: GetCourseWithDetailsContent | null | undefined;
    isLoading = false;
    videoSource = '';
    private lightGallery: LightGallery | undefined;
    ngOnInit(): void {
        this.idCourse = Number(this.router.snapshot.paramMap.get('id'));
        this.loadDataOfContent(this.idCourse);
    }

    settings = {
        counter: false,
        plugins: [lgVideo],
        download: false,
        dynamic: true,
    };
    onBeforeSlide = (detail: BeforeSlideDetail): void => {
        const { index, prevIndex } = detail;
        console.log(index, prevIndex);
    };

    // Tabs
    currentTab = 'tab1';
    switchTab(event: MouseEvent, tab: string) {
        event.preventDefault();
        this.currentTab = tab;
    }

    onInit = (detail): void => {
        this.lightGallery = detail.instance;
    };

    openGallery = () => {
        this.lightGallery.openGallery();
    };

    loadDataOfContent(id: number) {
        this.isLoading = true;
        this.courseServices.getCourses(id).subscribe((res) => {
            if (res.retCode === 0 && res.systemMessage === '') {
                this.courseContentCourse = res.data;
                this.isLoading = false;
                const itemGalley: any[] = [
                    {
                        sizes: '1280-720',
                        video: {
                            source: [
                                {
                                    src: this.courseContentCourse.fileUploadUrlStream,
                                    type: 'video/mp4'
                                }
                            ],
                            tracks: [],
                            attributes: { preload: 'none', controls: true }
                        }
                    }
                ];
                this.lightGallery.refresh(itemGalley);
            } else {
                this.isLoading = false;
            }
        });
    }


    addCourseToCart(){
        this.cartServices.addCourse(this.courseContentCourse);
    }

    get isMemberAccount(){
        return this.authenticationServices.isCheckMemberAccount();
    }

}