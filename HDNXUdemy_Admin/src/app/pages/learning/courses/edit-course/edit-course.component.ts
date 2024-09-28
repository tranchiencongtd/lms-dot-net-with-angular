import { Component } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import ClassicEditor from "@ckeditor/ckeditor5-build-classic";
import { DropzoneConfigInterface } from 'ngx-dropzone-wrapper';
import { CategoryServices } from 'src/app/core/services/category.service';
import { CourseServices } from 'src/app/core/services/course.service';
import { MessengerServices } from 'src/app/core/services/messenger.service';
import { Messenger } from 'src/app/models/contants/ennum_router';
import { CategoryModel } from 'src/app/models/models/category';
import { Course } from 'src/app/models/models/course';
import { ReturnUploadFile } from 'src/app/models/respone_model/return-upload-file';
import { UploadFileToCloud } from 'src/app/models/respone_model/upload-file-images-to-cloud';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-edit-course',
  templateUrl: './edit-course.component.html',
  styleUrls: ['./edit-course.component.scss'],
})

// Create component
export class EditCourseComponent {

  filesPicture: File;
  filesVideo: File;
  courseInsertData: Course ={
    title: '',
    shortDescription: '',
    isFree: false,
    introduce: '',
    levelCourse: '',
    languages: '',
    description: '',
    publicId: '',
    pictureUrl: '',
    keyVideoUpload: '',
    fileUrl: '',
    processCourse: 0,
  };
  courseForm!: UntypedFormGroup;
  public Editor = ClassicEditor;
  uploadedFileImages: UploadFileToCloud = {
    urlPicture: 'assets/images/real-estate/img-01.jpg',
    publicId: ''
  };
  uploadedVideoFile: ReturnUploadFile;
  // bread crumb items
  breadCrumbItems!: Array<{}>;
  categories: CategoryModel[] = [];
  nameCategory: string;
  isCreateCourse = false;
  isLoading = false;

  ngOnInit(): void {
    const id = Number(this.routers.snapshot.paramMap.get('id'));
    this.breadCrumbItems = [
      { label: 'Course', active: true, linkRouter: '/learning/list-course', isRouter: true },
      { label: 'Edit Course', active: true, linkRouter: '', isRouter: false },
    ];

    this.courseForm = this.formBuilder.group({
      id: [],
      title: [],
      shortDescription: [],
      idAuthor: [],
      totalStudentRegister: [],
      totalChapter: [],
      duration: [],
      priceOfCourse: [],
      isDiscount: [],
      priceOfDiscount: [],
      isFree: [],
      typeOfCourse: ['0'],
      introduce: [],
      idCategory: [0],
      levelCourse: [],
      languages: [0],
      description: [],
    });
    this.loadDataCategory();
    this.loadDataOfCourse(id);

    this.courseForm.controls['idCategory'].valueChanges.subscribe((value) => {
      this.nameCategory = this.categories.find(x => x.id == value)?.name ?? '';
    });
  }

  constructor(
    private router: Router,
    private formBuilder: UntypedFormBuilder,
    private categoriesServices: CategoryServices,
    private courseServices: CourseServices,
    private readonly messengerService: MessengerServices,
    private readonly routers: ActivatedRoute,) { }

  // File Upload
  public dropzoneImagesConfig: DropzoneConfigInterface = {
    url: `${environment.baseUrl}upload-to-cloud/cloudinary-request`,
    clickable: true,
    addRemoveLinks: true,
    acceptedFiles: 'image/*',
    maxFiles: 1,
  };

  public dropzoneVideoConfig: DropzoneConfigInterface = {
    url: `${environment.urlUploadFile}upload-video-to-server/file-document`,
    clickable: true,
    addRemoveLinks: true,
    acceptedFiles: 'video/*',
    maxFiles: 1,
    init: function () {
      const dropzone = this;
      dropzone.on('success', (file, response) => {
        const thumbnailElement = file.previewElement.querySelector('.dz-image img');
        if (thumbnailElement) {
          thumbnailElement.setAttribute('src', 'assets/images/default_icon.png');
        }
      });

      dropzone.on('error', (file, response) => {
        const thumbnailElement = file.previewElement.querySelector('.dz-image img');
        if (thumbnailElement) {
          thumbnailElement.setAttribute('src', 'assets/images/default_icon.png');
        }
      });
    },
  };

  uploadedPictureFiles: any[] = [];
  uploadedVideoFiles: any[] = [];
  // File Upload
  imageURL: any;
  onUploadFileImagesSuccess(event: any) {
    this.uploadedFileImages = event[1].data as UploadFileToCloud;
    this.uploadedPictureFiles.push(event[0]);
  }

  onUploadFileVideoSuccess(event: any) {
    this.uploadedVideoFile = event[1].data as ReturnUploadFile;
    this.uploadedVideoFiles.push(event[0]);
  }

  // File Remove
  removeFile(event: any) {
    this.uploadedPictureFiles.splice(this.uploadedPictureFiles.indexOf(event), 1);
  }

  public items = ['Bắt đầu', 'Trung bình', 'Cấp cao', 'Chuyên gia']

  addDetailsForCourse(id: number) {
    this.router.navigate([`/learning/courses/create-course-details/${id}`]);
  }

  saveCourse() {
    var idCourse = this.courseForm.controls['id'].value;
    if (this.courseForm.valid && this.checkValidate) {
      const dataInsert: Course = {
        id: idCourse,
        title: this.courseForm.controls['title'].value,
        shortDescription: this.courseForm.controls['shortDescription'].value,
        totalChapter: this.courseForm.controls['totalChapter'].value,
        duration: this.courseForm.controls['duration'].value,
        priceOfCourse: this.courseForm.controls['priceOfCourse'].value,
        isDiscount: this.courseForm.controls['isDiscount'].value,
        priceOfDiscount: this.courseForm.controls['priceOfDiscount'].value,
        isFree: this.courseForm.controls['isFree'].value,
        typeOfCourse: this.courseForm.controls['typeOfCourse'].value,
        introduce: this.courseForm.controls['introduce'].value,
        idCategory: this.courseForm.controls['idCategory'].value,
        levelCourse: this.courseForm.controls['levelCourse'].value.toString(),
        languages: this.courseForm.controls['languages'].value,
        description: this.courseForm.controls['description'].value,
        publicId: this.uploadedFileImages.publicId,
        pictureUrl: this.uploadedFileImages.urlPicture,
        keyVideoUpload: this.uploadedVideoFile.keyOfFile,
        fileUrl: this.uploadedVideoFile.fileName,
        processCourse: 0
      };
      if (idCourse != null && idCourse != 0 && idCourse != "" && idCourse != undefined) {
        dataInsert.processCourse = this.courseInsertData.processCourse;
        this.courseServices.updateInformationCourse(dataInsert).subscribe((res) => {
          if (res.retCode == 0 && res.systemMessage == "") {
            this.messengerService.successes(Messenger.createDataSuccessFull);
          } else {
            this.messengerService.errorWithIssue();
          }
        });
      } else {
        dataInsert.processCourse = 0;
        this.courseServices.createCourse(dataInsert).subscribe((res) => {
          if (res.retCode == 0 && res.systemMessage == "") {
            this.messengerService.successes(Messenger.updateSuccessFull);
          } else {
            this.messengerService.errorWithIssue();
          }
        });
      }
    }
  }

  loadDataCategory() {
    this.categoriesServices.getCategories().subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.categories = res.data;
      }
    })
  }

  private checkValidate(): boolean {
    if (this.uploadedFileImages.publicId && this.uploadedVideoFile.keyOfFile) {
      return true;
    }
    return false;
  }

  loadDataOfCourse(id: number) {
    this.uploadedPictureFiles = [];
    this.isLoading = true;
    this.courseServices.getCourses(id).subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.courseInsertData = res.data;
        var dataUploadOnCloud: UploadFileToCloud = {
          publicId: this.courseInsertData.publicId,
          urlPicture: this.courseInsertData.pictureUrl
        }
        var dataFileVideoUpload : ReturnUploadFile = {
          isUpload: true,
          fileName: this.courseInsertData.fileUrl,
          keyOfFile: this.courseInsertData.keyVideoUpload,
          fileSize: ''
        }
        this.uploadedFileImages = dataUploadOnCloud;
        this.uploadedPictureFiles.push(dataUploadOnCloud);
        this.uploadedVideoFiles.push(dataFileVideoUpload);
        this.courseForm.patchValue(this.courseInsertData);
        this.isLoading = false;
      } else {
        this.isLoading = false;
      }
    });
  }

}
