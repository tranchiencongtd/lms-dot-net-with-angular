import { Component, ViewChild } from '@angular/core';
import { DecimalPipe } from '@angular/common';

import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { DropzoneConfigInterface } from 'ngx-dropzone-wrapper';
import { FilemanagerModel } from 'src/app/models/models/file-manager.model';
import { UploadFileToServerServices } from 'src/app/core/services/upload-file.service';
import { FileTypeUpload } from 'src/app/models/respone_model/file-type-upload';
import { environment } from 'src/environments/environment';
import { ReturnUploadFile } from 'src/app/models/respone_model/return-upload-file';
import { UploadFileCloudServices } from 'src/app/core/services/upload-file-cloud.service';
import { MessengerServices } from 'src/app/core/services/messenger.service';

@Component({
  selector: 'app-list-file-upload',
  templateUrl: './list-file-upload.component.html',
  styleUrls: ['./list-file-upload.component.scss'],
  providers: [DecimalPipe,]
})

// Product Component
export class ListFileUploadComponent {

  folderData: any;
  recentDatas: any;
  fliefolders: any;
  file: any;
  storageChart: any;
  sortValue: any = "Docs Type";
  files: File[] = [];
  endItem: any;
  fileTypeUploads: FileTypeUpload[] = [];
  fileOfTypeUploads: FileTypeUpload[] = [];
  fileSoftwares: FilemanagerModel[] = [];
  listFileSoftwares: FilemanagerModel[] = [];
  fileUpload!: UntypedFormGroup;
  fileManagerUpload: FilemanagerModel;
  resultUpload: ReturnUploadFile;
  idFileUpload: number;

  @ViewChild('uploadFile', { static: false }) uploadFile?: ModalDirective;
  constructor(
    private formBuilder: UntypedFormBuilder,
    private readonly uploadFileToServerServices: UploadFileToServerServices,
    private readonly uploadFileToCloudServices: UploadFileCloudServices,
    private readonly messengerService: MessengerServices,) {
    this.fileUpload = this.formBuilder.group({
      id: [''],
      fileName: [],
      fileType: [0],
      fileUrl: [],
      actualNameFile: [''],
      extendFile: [0],
      isFree: [true]
    });
  }

  ngOnInit(): void {

    document.body.classList.add('file-detail-show');
    this.getTypeFileUpload();
    this.getListFileOfType();
    this.loadDataOfSoftware();
  }

  // file upload
  public dropzoneConfig: DropzoneConfigInterface = {
    url: `${environment.urlUploadFile}upload-video-to-server/file-document`,
    maxFiles: 1,
    addRemoveLinks: true,
    init: function () {
      const dropzone = this;
      dropzone.on('success', (file, response) => {
        const thumbnailElement = file.previewElement.querySelector('.dz-image img');
        if (thumbnailElement) {
          thumbnailElement.setAttribute('src', 'assets/images/default_icon.png');
        }
      });
    },
  };

  getTypeFileUpload() {
    this.uploadFileToServerServices.getFileTypeUpload().subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.fileTypeUploads = res.data;
      }
    });
  }


  getListFileOfType() {
    this.uploadFileToServerServices.getFileOfTypeUpload().subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.fileOfTypeUploads = res.data;
      }
    });
  }

  saveFileUpload() {
    var idFile = this.fileUpload.controls['id'].value;
    if (this.fileUpload.valid && this.checkValidate) {
      this.uploadFile?.hide();
      const dataInsert: FilemanagerModel = {
        id: idFile,
        fileName: this.fileUpload.value.fileName,
        fileType: this.fileUpload.value.fileType,
        fileUrl: this.resultUpload.fileName,
        actualNameFile: this.fileUpload.value.actualNameFile,
        isFree: this.fileUpload.value.isFree,
        extendFile: this.fileUpload.value.extendFile,
        keyOfFile: this.resultUpload.keyOfFile,
        fileSize: this.resultUpload.fileSize
      }
      if (idFile != null && idFile != 0 && idFile != "" && idFile != undefined) {
        this.uploadFileToCloudServices.updateInformationSoftware(dataInsert).subscribe((res) => {
          if (res.retCode == 0 && res.systemMessage == "") {
            this.loadDataOfSoftware();
            this.fileUpload.reset();
          } else {
            this.messengerService.errorWithIssue();
          }
        });
      } else {
        this.uploadFileToCloudServices.createFileSoftware(dataInsert).subscribe((res) => {
          if (res.retCode == 0 && res.systemMessage == "") {
            this.loadDataOfSoftware();
            this.fileUpload.reset();
          } else {
            this.messengerService.errorWithIssue();
          }
        });
      }
    }
  }


  private checkValidate(): boolean {
    if (this.resultUpload.keyOfFile) {
      return true;
    }
    if (this.resultUpload.isUpload) {
      return true;
    }
    return false;
  }

  uploadedFiles: any[] = [];

  // File Upload
  imageURL: any;
  onUploadSuccess(event: any) {
    this.resultUpload = event[1].data as ReturnUploadFile;
  }


  loadDataOfSoftware() {
    this.uploadFileToCloudServices.getListFileSoftware().subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.fileSoftwares = res.data;
        this.listFileSoftwares = res.data;
        this.fileSoftwares = this.listFileSoftwares.slice(0, 15);
        document.getElementById('elmLoader')?.classList.add('d-none');
      } else {
        document.getElementById('elmLoader')?.classList.add('d-none');
      }

    });
  }

  // File Remove
  removeFile(event: any) {
    this.uploadedFiles.splice(this.uploadedFiles.indexOf(event), 1);
  }

  // Open Detail
  openDetail(id: any) {
    (document.querySelector('.file-manager-right-wrapper') as HTMLElement).style.display = 'block'
  }

  // Close Detail
  closeDetail() {
    (document.querySelector('.file-manager-right-wrapper') as HTMLElement).style.display = 'none'
  }

  direction: any = 'asc';
  sortKey: any = '';
  sortBy(column: any, value: any) {
    this.sortValue = value;
    this.onSort(column)
  }

  onSort(column: any) {
    if (this.direction == 'asc') {
      this.direction = 'desc';
    } else {
      this.direction = 'asc';
    }
    const sortedArray = [...this.recentDatas]; // Create a new array
    sortedArray.sort((a, b) => {
      const res = this.compare(a[column], b[column]);
      return this.direction === 'asc' ? res : -res;
    });
    this.recentDatas = sortedArray;
  }
  compare(v1: string | number, v2: string | number) {
    return v1 < v2 ? -1 : v1 > v2 ? 1 : 0;
  }

  // Pagination
  pageChanged(event: any): void {
    const startItem = (event.page - 1) * event.itemsPerPage;
    this.endItem = event.page * event.itemsPerPage;
    this.fileSoftwares = this.listFileSoftwares.slice(startItem, this.endItem);
  }

  editFileUpload(id: number) {
    this.uploadedFiles = [];
    this.idFileUpload = id;
    this.uploadFile?.show();
    var modaltitle = document.querySelector('.modal-title') as HTMLAreaElement
    modaltitle.innerHTML = 'Chỉnh sửa file upload'
    var modalButton = document.getElementById('add-btn') as HTMLAreaElement
    modalButton.innerHTML = 'Cập nhật';
    var dataEdit = this.fileSoftwares.find(x => x.id == id);
    this.fileUpload.controls['isFree'].setValue(dataEdit.isFree);
    this.fileUpload.patchValue(dataEdit);
    this.uploadedFiles.push(dataEdit);
  }

  addFileUpload() {
    this.uploadedFiles = [];
    this.uploadFile?.show();
    var modaltitle = document.querySelector('.modal-title') as HTMLAreaElement
    modaltitle.innerHTML = 'Tạo file'
    var modalButton = document.getElementById('add-btn') as HTMLAreaElement
    modalButton.innerHTML = 'Lưu';
    this.fileUpload.controls['isFree'].setValue(true);
  }
}
