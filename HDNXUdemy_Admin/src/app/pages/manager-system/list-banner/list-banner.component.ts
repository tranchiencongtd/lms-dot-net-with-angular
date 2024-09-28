import { Component, ViewChild } from '@angular/core';
import { DecimalPipe } from '@angular/common';

import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { DropzoneConfigInterface } from 'ngx-dropzone-wrapper';
import { Store } from '@ngrx/store';
import { MessengerServices } from 'src/app/core/services/messenger.service';
import { UploadFileCloudServices } from 'src/app/core/services/upload-file-cloud.service';
import { environment } from 'src/environments/environment';
import { v4 as uuidv4 } from 'uuid';
import { BannerServices } from 'src/app/core/services/banner.service';
import { Banner } from 'src/app/models/models/banner';

@Component({
  selector: 'app-list-banner',
  templateUrl: './list-banner.component.html',
  styleUrls: ['./list-banner.component.scss'],
  providers: [DecimalPipe,]
})

// Product Component
export class ListBannerComponent {

  // bread crumb items
  breadCrumbItems!: Array<{}>;
  banner: Banner[] = [];
  file: File;
  bannerForm!: UntypedFormGroup;
  isUploadImages = false;
  term: any;
  bannerList: Banner[] = [];
  publicId: string;
  pictureUrl: string;
  idCategory: number;
  isActive: boolean = true;

  @ViewChild('addCategory', { static: false }) addCategory?: ModalDirective;
  @ViewChild('deleteRecordModal', { static: false }) deleteRecordModal?: ModalDirective;
  constructor(public store: Store,
    private formBuilder: UntypedFormBuilder,
    private readonly bannerService: BannerServices,
    private readonly messengerService: MessengerServices,
    private readonly uploadDataToCloud: UploadFileCloudServices) { }


  ngOnInit(): void {
    /**
     * BreadCrumb
     */
    this.breadCrumbItems = [
      { label: 'Manager System', active: true },
      { label: 'List of Banner', active: true }
    ];

    /**
      * Form Validation
    */
    this.bannerForm = this.formBuilder.group({
      id: [''],
      pictureUrl: [],
      publicId: [],
      numberCourse: [],
      contentBanner: [''],
      link: [''],
      isActive: [true]
    });
    this.loadDataCategory();
  }

  loadDataCategory() {
    document.getElementById('elmLoader')?.classList.remove('d-none');
    this.bannerService.getBanners().subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.banner = res.data;
        this.bannerList = res.data;;
        this.banner = this.bannerList.slice(0, 15);
        document.getElementById('elmLoader')?.classList.add('d-none');
      }
    })
  }


  // File Upload
  public dropzoneConfig: DropzoneConfigInterface = {
    url: `${environment.baseUrl}upload-to-cloud/cloudinary-request`,
    clickable: true,
    maxFiles: 1,
    addRemoveLinks: true,
    renameFile: (file) => {
      return uuidv4().toString();
    },
  };

  uploadedFiles: any[] = [];

  // File Upload
  imageURL: any;
  onUploadSuccess(event: any) {
    this.file = event[0];
    this.publicId = event[1].data.publicId;
    this.pictureUrl = event[1].data.urlPicture;
    this.isUploadImages = true;
  }

  // File Remove
  removeFile(event: any) {
    if (this.publicId) {
      this.uploadDataToCloud.deleteImageOnCloud(this.publicId).subscribe((res) => {
        if (res.retCode == 0 && res.systemMessage == "") {
          this.pictureUrl = "";
          this.publicId = "";
          this.isUploadImages = false;
        }
      })
    }
  }

  // Add Category
  actionCategory() {
    var idCategory = this.bannerForm.controls['id'].value;
    if (this.bannerForm.valid && this.checkValidate) {
      this.addCategory?.hide();
      const dataInsert: Banner = {
        id: idCategory,
        contentBanner: this.bannerForm.value.contentBanner,
        urlPicture: this.pictureUrl,
        publicId: this.publicId,
        isActive: this.bannerForm.value.isActive,
        link: this.bannerForm.value.link
      }
      if (idCategory != null && idCategory != 0 && idCategory != "" && idCategory != undefined) {
        this.bannerService.updateBanner(dataInsert).subscribe((res) => {
          if (res.retCode == 0 && res.systemMessage == "") {
            this.loadDataCategory();
            this.bannerForm.reset();
          } else {
            this.messengerService.errorWithIssue();
          }
        });
      } else {
        this.bannerService.createBanner(dataInsert).subscribe((res) => {
          if (res.retCode == 0 && res.systemMessage == "") {
            this.loadDataCategory();
            this.bannerForm.reset();
          } else {
            this.messengerService.errorWithIssue();
          }
        });
      }
    }
  }


  private checkValidate(): boolean {
    if (this.pictureUrl) {
      return true;
    }
    if (this.isUploadImages) {
      return true;
    }
    return false;
  }
  // filterdata
  filterdata() {
    if (this.term) {
      this.banner = this.bannerList.filter((el: any) => el.name.toLowerCase().includes(this.term.toLowerCase()))
    } else {
      this.banner = this.bannerList.slice(0, 15)
    }
    // noResultElement
    this.updateNoResultDisplay();
  }

  // no result 
  updateNoResultDisplay() {
    const noResultElement = document.querySelector('.noresult') as HTMLElement;
    const paginationElement = document.getElementById('pagination-element') as HTMLElement
    if (this.term && this.banner.length === 0) {
      noResultElement.style.display = 'block';
      paginationElement.classList.add('d-none')
    } else {
      noResultElement.style.display = 'none';
      paginationElement.classList.remove('d-none')
    }
  }

  // Page Changed
  pageChanged(event: any): void {
    const startItem = (event.page - 1) * event.itemsPerPage;
    const endItem = event.page * event.itemsPerPage;
    this.banner = this.bannerList.slice(startItem, endItem);
  }

  addCategoryModal() {
    this.bannerForm.reset();
    this.publicId = "";
    this.pictureUrl = "";
    this.addCategory?.show();
    var modaltitle = document.querySelector('.modal-title') as HTMLAreaElement
    modaltitle.innerHTML = 'Thêm hình ảnh banner'
    var modalButton = document.getElementById('add-btn') as HTMLAreaElement
    modalButton.innerHTML = 'Lưu';
  }

  editCategory(id: number) {
    this.idCategory = id;
    this.addCategory?.show();
    var modaltitle = document.querySelector('.modal-title') as HTMLAreaElement
    modaltitle.innerHTML = 'Chỉnh sửa ảnh banner'
    var modalButton = document.getElementById('add-btn') as HTMLAreaElement
    modalButton.innerHTML = 'Cập nhật';
    var dataEdit = this.banner.find(x => x.id == id);
    this.publicId = dataEdit.publicId;
    this.pictureUrl = dataEdit.urlPicture;
    this.bannerForm.controls['isActive'].setValue(dataEdit.isActive);
    this.bannerForm.patchValue(dataEdit);
  }
}
