import { Component, ElementRef, OnInit, ViewChild, inject } from '@angular/core';
import "@mux/mux-player";
import { BeforeSlideDetail } from 'lightgallery/lg-events';
import lgVideo from 'lightgallery/plugins/video';
import { UserCommentCoursesComponent } from '../../dialog/user-comment-courses/user-comment-courses.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserReviewCoursesComponent } from '../../dialog/user-review-courses/user-review-courses.component';
import Artplayer from 'artplayer';
import { type Option } from 'artplayer/types/option';
import Hls from 'hls.js';
import { CourseServices } from 'src/app/core/services/course.service';
import { MessengerServices } from 'src/app/core/services/messenger.service';
import { ActivatedRoute } from '@angular/router';
import { GetCourseWithDetailsContent } from 'src/app/models/respone_model/course-content-with-detail';
import { CourseContentDetails } from 'src/app/models/models/course-content';
import { CourseEvaluation } from 'src/app/models/models/course-evaluation';
import { ConstantValue } from 'src/app/models/contants/ennum_router';
import { LocalStorageConfig } from 'src/app/library/clientconfig/localstorageconfig';
import { TheadQuestionCourse } from 'src/app/models/models/thead-question-course';
import { StudentServices } from 'src/app/core/services/student.service';

@Component({
  selector: 'app-courses-content-detail',
  templateUrl: './courses-content-detail.component.html',
  styleUrls: ['./courses-content-detail.component.scss'],
  host: { class: 'd-block' },
})
export class CoursesContentDetailComponent implements OnInit {

  private modalService = inject(NgbModal);
  isDisplayReplay = false;
  isComment = false;
  @ViewChild('artplayer') artplayerElement: ElementRef;
  player: Artplayer | undefined;
  playerHeight = '100%';
  option: Option = {
    container: '',
    url: '',
    volume: 0.5,
    isLive: false,
    muted: false,
    autoplay: true,
    pip: true,
    autoMini: true,
    setting: true,
    loop: true,
    flip: true,
    playbackRate: true,
    aspectRatio: true,
    fullscreen: true,
    fullscreenWeb: true,
    subtitleOffset: true,
    miniProgressBar: true,
    mutex: true,
    backdrop: true,
    playsInline: true,
    autoPlayback: true,
    airplay: true,
    theme: '#23ade5',
    lang: navigator.language.toLowerCase(),
    hotkey: true,
    type: 'm3u8',
    customType: {
      m3u8: function playM3u8(video, url, art) {
        if (Hls.isSupported()) {
          if (art.hls) art.hls.destroy();
          const hls = new Hls();
          hls.loadSource(url);
          hls.attachMedia(video);
          art.hls = hls;
          art.on('destroy', () => hls.destroy());
        } else if (video.canPlayType('application/vnd.apple.mpegurl')) {
          video.src = url;
        } else {
          art.notice.show = 'Unsupported playback format: m3u8';
        }
      }
    },
  }
  idCourse = 0;
  contentCourseDetail: GetCourseWithDetailsContent | null | undefined;
  videoOfContent: CourseContentDetails;
  activeItemIndex: number | null = null;
  commentOfCourse: CourseEvaluation[] = [];
  theadQuestionOfCourse: TheadQuestionCourse[] = [];
  listNameRegister: string[] = [];

  constructor(
    private readonly courseServices: CourseServices,
    private readonly messengerService: MessengerServices,
    private readonly router: ActivatedRoute,
    private readonly studentServices: StudentServices,
  ) { }

  ngOnInit() {
    this.idCourse = Number(this.router.snapshot.paramMap.get('id'));
    this.loadDataForCourse(this.idCourse);
    this.getListCommentOfCourse(this.idCourse);
    this.getListTheadQuestionOfCourse(this.idCourse);
    this.getListUserNameRegisterForCourse(this.idCourse);
  }

  ngAfterViewInit(): void {
    this.option.container = this.artplayerElement.nativeElement;
    if (this.artplayerElement) {
      this.player = new Artplayer(this.option);
    } else {

    }

  }

  selectedVideoUrl: string;

  selectCourse(course: { videoUrl: string; }) {
    this.selectedVideoUrl = course.videoUrl;
  }

  settings = {
    counter: false,
    plugins: [lgVideo]
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

  openModalCommentUser() {
    const modalRef = this.modalService.open(UserCommentCoursesComponent, { size: 'xl' });
    modalRef.componentInstance.name = 'World';
  }

  openModalReviewCourses() {
    let getValueLocalStor = LocalStorageConfig.GetUser();
    const modalRef = this.modalService.open(UserReviewCoursesComponent, { size: 'xl' });
    modalRef.componentInstance.reviewOfUser = '';
    modalRef.componentInstance.idCourse = this.idCourse;
    modalRef.componentInstance.idStudent = getValueLocalStor.userId;
    modalRef.closed.subscribe((res) => {
      if (res == ConstantValue.finishModal) {
        this.getListCommentOfCourse(this.idCourse);
        this.loadDataForCourse(this.idCourse);
      } else {
        this.messengerService.errorWithIssue();
      }
    })
  }

  replayComment() {
    this.isDisplayReplay = true;
  }

  getDetailDataOfCourse() {

  }

  loadDataForCourse(id: number) {
    this.courseServices.getCourses(id).subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.contentCourseDetail = res.data;
        this.player.url = this.contentCourseDetail.fileUploadUrlStream;
      } else {
        this.messengerService.errorWithIssue();
      }
    });
  }

  getDataOfContentDetails(id: number) {
    this.courseServices.getContentCourseDetails(id).subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.videoOfContent = res.data;
        this.player.url = this.videoOfContent.fileUploadUrlStream;
      } else {
        this.messengerService.errorWithIssue();
      }
    });
  }

  onClickContentCourse(id: number) {
    this.activeItemIndex = id;
  }

  getListCommentOfCourse(idCourse: number) {
    this.courseServices.getListCourseEvaluation(idCourse).subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.commentOfCourse = res.data;
        this.player.url = this.videoOfContent?.fileUploadUrlStream;
      } else {
        this.messengerService.errorWithIssue();
      }
    });
  }

  likeForComment(id: number) {
    this.courseServices.likeForCommentCourse(id).subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.getListCommentOfCourse(this.idCourse);
      } else {
        this.messengerService.errorWithIssue();
      }
    });
  }

  disLikeForComment(id: number) {
    this.courseServices.disLikeForCommentCourse(id).subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.getListCommentOfCourse(this.idCourse);
      } else {
        this.messengerService.errorWithIssue();
      }
    });
  }

  getListTheadQuestionOfCourse(idCourse: number) {
    this.courseServices.getListTheadQuestionCourse(idCourse).subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.theadQuestionOfCourse = res.data;
      } else {
        this.messengerService.errorWithIssue();
      }
    });
  }

  getListUserNameRegisterForCourse(idCourse: number) {
    this.studentServices.getListUserNameRegisterForCourse(idCourse).subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.listNameRegister = res.data;
      } else {
        this.messengerService.errorWithIssue();
      }
    });
  }

}
