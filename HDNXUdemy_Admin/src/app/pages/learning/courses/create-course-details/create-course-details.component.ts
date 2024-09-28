import { ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { Observable } from 'rxjs';
// Data get
import { Options } from '@angular-slider/ngx-slider';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Store } from '@ngrx/store';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { deleteproductsList } from 'src/app/store/Product/product.action';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { DropzoneComponent, DropzoneConfigInterface } from 'ngx-dropzone-wrapper';
import { CourseServices } from 'src/app/core/services/course.service';
import { CourseContent, CourseContentDetails } from 'src/app/models/models/course-content';
import { ContentAndDetails, CourseContentWithDetails } from 'src/app/models/respone_model/course-content-with-detail';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MessengerServices } from 'src/app/core/services/messenger.service';
import { environment } from 'src/environments/environment';
import { ReturnUploadFile } from 'src/app/models/respone_model/return-upload-file';
import Hls from 'hls.js';
import { type Option } from 'artplayer/types/option';
import Artplayer from 'artplayer';
import { Messenger } from 'src/app/models/contants/ennum_router';

@Component({
  selector: 'app-create-course-details',
  templateUrl: './create-course-details.component.html',
  styleUrls: ['./create-course-details.component.scss'],
  providers: [DecimalPipe]
})
export class CreateCourseDetailsComponent {
  @ViewChild('addContentCourseModal', { static: false }) addContentCourseModal?: ModalDirective;
  @ViewChild('artplayerCreateCourseDetails') artplayerCreateCourseDetails: ElementRef;
  @ViewChild(DropzoneComponent) dropzone: DropzoneComponent;
  // bread crumb items
  breadCrumbItems!: Array<{}>;
  allproducts!: any;

  searchResults: any;
  searchTerm: any;
  isLoading = false;
  isLoadingContent = false;
  listContentCourse: ContentAndDetails[] = [];
  contentCourseForm!: UntypedFormGroup;
  contentDetailsCourseForm!: UntypedFormGroup;
  dataInsertContent: CourseContent;
  idCourse: number;
  idContent: number;
  isShowEditContent: boolean = false;
  uploadedVideoFile: ReturnUploadFile;
  fileLoadUpload: File;
  player: Artplayer | undefined;
  option: Option = {
    container: '',
    url: '',
    volume: 0.5,
    isLive: false,
    muted: false,
    autoplay: false,
    pip: true,
    autoSize: true,
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
  isReviewVideo = false;
  isArtplayerInitialized = false;
  contentCourseDetail: CourseContentDetails;
  contentName: string;
  activeItemIndex: number | null = null;



  constructor(
    public store: Store,
    private formBuilder: UntypedFormBuilder,
    private courseServices: CourseServices,
    private readonly routers: ActivatedRoute,
    private readonly messengerService: MessengerServices,
    private cdr: ChangeDetectorRef) {
  }

  ngAfterViewInit(): void {
    if (!this.isArtplayerInitialized && this.artplayerCreateCourseDetails) {
      this.option.container = this.artplayerCreateCourseDetails.nativeElement;
      this.player = new Artplayer(this.option);
      this.isArtplayerInitialized = true;
    } else {
      console.error('artplayerElement is undefined!');
    }

  }

  ngOnInit(): void {
    this.idCourse = Number(this.routers.snapshot.paramMap.get('idCourse'));
    this.breadCrumbItems = [
      { label: 'Course', active: true },
      { label: 'Content of course', active: true }
    ];


    this.contentCourseForm = this.formBuilder.group({
      id: [''],
      contentCourse: [],
    });

    this.contentDetailsCourseForm = this.formBuilder.group({
      id: [''],
      nameSubContent: [],
      isLearningFree: [],
    })
    this.loadDataContentOfCourse(this.idCourse);


  }

  loadDataContentOfCourse(idCourse: number) {
    this.isLoading = true;
    this.courseServices.getListContentCourse(idCourse).subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == '') {
        this.listContentCourse = res.data;
        this.isLoading = false;
      } else {
        this.isLoading = false;
      }
    })
  }


  // Search Data
  performSearch(): void {
    // this.searchResults = this.productlist.filter((item: any) => {
    //   return item.category.toLowerCase().includes(this.searchTerm.toLowerCase())
    //     || item.title.toLowerCase().includes(this.searchTerm.toLowerCase())
    // })
    // this.products = this.searchResults.slice(0, 10);
    if (this.searchResults.length == 0) {
      (document.getElementById('search-result-elem') as HTMLElement).style.display = 'block'
    } else {
      (document.getElementById('search-result-elem') as HTMLElement).style.display = 'none'
    }
  }

  public dropzoneVideoConfig: DropzoneConfigInterface = {
    url: `${environment.urlUploadFile}upload-video-to-server/video-course`,
    clickable: true,
    addRemoveLinks: true,
    acceptedFiles: 'video/*',
    maxFiles: 1,
    init: function () {
      const dropzone = this;
      dropzone.on('success', (file, response) => {
        const thumbnailElement = file.previewElement.querySelector('.dz-image');
        if (thumbnailElement) {
          // thumbnailElement.setAttribute('src', 'assets/images/default_icon.png');
          thumbnailElement.setAttribute('style', 'height: 200px;');
        }
      });

      dropzone.on('error', (file, response) => {
        const thumbnailElement = file.previewElement.querySelector('.dz-image');
        if (thumbnailElement) {
          thumbnailElement.setAttribute('style', 'height: 200px;');
        }
      });
    },
  };

  public Editor = ClassicEditor;
  public items = ['Beginner', 'Advanced', 'Intermediate', 'Expert'];
  uploadedFiles: any[] = [];

  // File Remove
  removeFile() {
    this.uploadedFiles.splice(this.uploadedFiles.indexOf(event), 1);
    this.fileLoadUpload = null;
    this.isReviewVideo = false;
    if (this.dropzone && this.dropzone.directiveRef) {
      this.dropzone.directiveRef.reset(); // This will reset the Dropzone and clear all files
    }
  }

  onUploadSuccess(event: any) {
    this.fileLoadUpload = event[0] as File;
    this.uploadedVideoFile = event[1].data as ReturnUploadFile;
    this.player.url = this.uploadedVideoFile.fileUploadUrlStream;
    this.isReviewVideo = true;
  }

  addContentCourse() {
    this.addContentCourseModal?.show();
  }

  saveContentCourse() {
    if (this.contentCourseForm.valid) {
      let dataInsert: CourseContent =
      {
        name: this.contentCourseForm.value.contentCourse,
        idCourse: this.idCourse
      };

      this.courseServices.createContentCourse(dataInsert).subscribe((res) => {
        if (res.retCode == 0 && res.systemMessage == "") {
          this.loadDataContentOfCourse(this.idCourse);
          this.contentCourseForm.reset();
          this.addContentCourseModal?.hide();
        } else {
          this.messengerService.errorWithIssue();
        }
      });
    }
  }

  addDetailForCourse(idContent: number) {
    this.isShowEditContent = true;
    this.idContent = idContent;
    this.isReviewVideo = false;
    this.contentDetailsCourseForm.reset();
  }

  saveContentCourseDetails() {
    this.isLoading = true;
    const dataInsert: CourseContentDetails = {
      idContent: this.idContent,
      nameSubContent: this.contentDetailsCourseForm.value.nameSubContent,
      timeOfContent: '',
      isLearningFree: this.contentDetailsCourseForm.value.isLearningFree ?? false,
      idVideoUpload: this.uploadedVideoFile.keyOfFile,
      fileNameVideo: this.uploadedVideoFile.fileName,
      isFinishConvert: true
    }
    if (this.contentDetailsCourseForm.valid) {
      this.courseServices.createContentCourseDetails(dataInsert).subscribe((res) => {
        if (res.retCode == 0 && res.systemMessage == "") {
          this.isLoading = false;
          this.loadDataContentOfCourse(this.idCourse);
          this.messengerService.successes(Messenger.createDataSuccessFull);
        } else {
          this.isLoading = false;
          this.loadDataContentOfCourse(this.idCourse);
          this.messengerService.errorWithIssue();
        }
      });
    } else {
      this.messengerService.warringWithIssue();
    }
  }

  getDataOfContentDetails(id: number) {
    this.isLoadingContent = true;
    this.isReviewVideo = true;
    this.isShowEditContent = true;
    this.courseServices.getContentCourseDetails(id).subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.contentCourseDetail = res.data;
        this.isLoadingContent = false;
        this.player.url = this.contentCourseDetail.fileUploadUrlStream;
        this.contentDetailsCourseForm.patchValue(this.contentCourseDetail);
      } else {
        this.isLoadingContent = false;
        this.messengerService.errorWithIssue();
      }
    });
  }

  clickHeaderOfList(idContent: number, contentName: string) {
    this.contentName = contentName;
    this.idContent = idContent;
  }

  updateContentCourse() {
    this.isLoading = true;
    const getDataUpdate = this.listContentCourse.find(x => x.id == this.idContent);
    getDataUpdate.name = this.contentName;
    this.courseServices.updateInformationContentCourse(getDataUpdate).subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.isLoading = false
        this.messengerService.successes(Messenger.updateSuccessFull);
      } else {
        this.isLoading = false
        this.messengerService.errorWithIssue();
      }
    })
  }

  onClickContentCourse(id: number) {
    this.activeItemIndex = id;
  }
}
