import { Component, ElementRef, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { DropzoneConfigInterface } from 'ngx-dropzone-wrapper';
import Artplayer from 'artplayer';
import { type Option } from 'artplayer/types/option';
import Hls from 'hls.js';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseServices } from 'src/app/core/services/course.service';
import { Course } from 'src/app/models/models/course';
import { MessengerServices } from 'src/app/core/services/messenger.service';
import { GetCourseWithDetailsContent } from 'src/app/models/respone_model/course-content-with-detail';
import { ETypeProcessCourse } from 'src/app/models/enum/etype_project.enum';
import { environment } from 'src/environments/environment';
import { CourseContentDetails } from 'src/app/models/models/course-content';
import { Messenger } from 'src/app/models/contants/ennum_router';
@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss']
})

// Overview Component
export class OverviewComponent {

  // bread crumb items
  breadCrumbItems!: Array<{}>;
  reviewForm!: UntypedFormGroup;
  reviewData: any;
  submitted: boolean = false;
  deleteId: any;
  files: File[] = [];
  rate: any;
  currentTab = 'description';
  idCourse: number;
  courseAndDetailContent: GetCourseWithDetailsContent;
  isLoading: boolean = false;
  activeItemIndex: number | null = null;
  contentCourseDetail: CourseContentDetails;

  @ViewChild('artplayer') artplayerElement: ElementRef;
  @ViewChild('addReview', { static: false }) addReview?: ModalDirective;
  @ViewChild('removeItemModal', { static: false }) removeItemModal?: ModalDirective;
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

  constructor(
    private formBuilder: UntypedFormBuilder,
    private readonly routers: ActivatedRoute,
    private readonly courseServices: CourseServices,
    private readonly messengerService: MessengerServices,
    private router: Router,) { }

  ngAfterViewInit(): void {
    this.option.container = this.artplayerElement.nativeElement;
    if (this.artplayerElement) {
      this.player = new Artplayer(this.option);
    } else {

    }

  }

  ngOnInit(): void {
    this.idCourse = Number(this.routers.snapshot.paramMap.get('idCourse'));
    this.breadCrumbItems = [
      { label: 'Course', active: true },
      { label: 'Overview Course', active: true }
    ];


    this.reviewForm = this.formBuilder.group({
      _id: [''],
      title: ['', [Validators.required]],
      content: ['', [Validators.required]],
      rate: ['', [Validators.required]],
      img: ['']
    });
    this.loadDataOfCourseOverview();
  }

  public dropzoneConfig: DropzoneConfigInterface = {
    url: `${environment.urlUploadFile}upload-video-to-server/file-document`,
    clickable: true,
    addRemoveLinks: true,
    previewsContainer: false,
  };

  uploadedFiles: any[] = [];

  // File Upload
  profile: any = [];
  onUploadSuccess(event: any) {
    setTimeout(() => {
      this.uploadedFiles.push(event[0]);
      this.profile.push(event[0].dataURL)
      this.reviewForm.controls['img'].setValue(this.uploadedFiles);
    }, 0);
  }

  // Change Tab Content
  changeTab(tab: string) {
    this.currentTab = tab;
  }


  // File Remove
  removeFile(event: any) {
    this.uploadedFiles.splice(this.uploadedFiles.indexOf(event), 1);
  }

  // open & close chatbot
  openChatBox() {
    document.getElementById('emailchat-detailElem')?.classList.add('d-block')
  }
  closeChatBox() {
    document.getElementById('emailchat-detailElem')?.classList.remove('d-block')
  }

  // Edit Review
  editReview(id: any) {
    this.uploadedFiles = []
    this.addReview?.show()
    this.reviewForm.controls['_id'].setValue(this.reviewData[id].id);
    this.reviewForm.controls['title'].setValue(this.reviewData[id].title);
    this.reviewForm.controls['rate'].setValue(this.reviewData[id].rating);
    this.reviewForm.controls['content'].setValue(this.reviewData[id].content);
    this.reviewData[id].profile.forEach((element: any) => {
      this.uploadedFiles.push({ 'dataURL': element, 'name': 'image', 'size': 1024, });
    });
    this.reviewForm.controls['img'].setValue(this.uploadedFiles);

  }

  // Delete Review
  removeReview(id: any) {
    this.deleteId = id
    this.removeItemModal?.show()
  }

  deleteReview() {
    this.reviewData.splice(this.deleteId, 1)
    this.removeItemModal?.hide()
  }

  loadDataOfCourseOverview() {
    this.isLoading = true;
    this.courseServices.getCourses(this.idCourse).subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.courseAndDetailContent = res.data;
        this.isLoading = false;
      } else {
        this.isLoading = false;
        this.messengerService.errorWithIssue();
      }
    })
  }

  onClickContentCourse(id: number) {
    this.activeItemIndex = id;
  }

  publicTheCourse() {
    this.isLoading = true;
    this.courseAndDetailContent.processCourse = ETypeProcessCourse.Public;
    this.courseServices.updateStatusCourse(this.courseAndDetailContent).subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.isLoading = false;
        this.messengerService.successes(Messenger.updateSuccessFull).then((result) => {
          if (result) {
            this.loadDataOfCourseOverview();
          }
        });
      } else {
        this.isLoading = false;
        this.messengerService.errorWithIssue();
      }
    })
  }

  goToEditCourse() {
    this.router.navigate([`/learning/courses/edit-course/${this.idCourse}`]);
  }

  getDataOfContentDetails(id: number) {
    this.courseServices.getContentCourseDetails(id).subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.contentCourseDetail = res.data;
        this.player.url = this.contentCourseDetail.fileUploadUrlStream;
      } else {
        this.messengerService.errorWithIssue();
      }
    });
  }
}
