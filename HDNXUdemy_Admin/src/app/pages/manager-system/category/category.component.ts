import { Component, ViewChild } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { DropzoneConfigInterface } from 'ngx-dropzone-wrapper';
import { Store } from '@ngrx/store';
import { environment } from 'src/environments/environment';
import { v4 as uuidv4 } from 'uuid';
import { CategoryServices } from 'src/app/core/services/category.service';
import { CategoryModel } from 'src/app/models/models/category';
import { MessengerServices } from 'src/app/core/services/messenger.service';
import { UploadFileCloudServices } from 'src/app/core/services/upload-file-cloud.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss'],
  providers: [DecimalPipe]

})
export class CategoryComponent {

  // bread crumb items
  breadCrumbItems!: Array<{}>;
  categories: CategoryModel[] = [];
  file: File;
  categoryForm!: UntypedFormGroup;
  isUploadImages = false;
  term: any;
  categorieslist: CategoryModel[] = [];
  publicId: string;
  pictureUrl: string;
  idCategory: number;

  @ViewChild('addCategory', { static: false }) addCategory?: ModalDirective;
  @ViewChild('deleteRecordModal', { static: false }) deleteRecordModal?: ModalDirective;
  constructor(public store: Store,
    private formBuilder: UntypedFormBuilder,
    private readonly categoryService: CategoryServices,
    private readonly messengerService: MessengerServices,
    private readonly uploadDataToCloud: UploadFileCloudServices) { }


  ngOnInit(): void {
    /**
     * BreadCrumb
     */
    this.breadCrumbItems = [
      { label: 'Quản lý khoá học', active: true },
      { label: 'Danh sách loại khoá học', active: true }
    ];

    /**
      * Form Validation
    */
    this.categoryForm = this.formBuilder.group({
      id: [''],
      pictureUrl: [],
      publicId: [],
      numberCourse: [],
      name: [''],
    });
    this.loadDataCategory();
  }

  loadDataCategory() {
    document.getElementById('elmLoader')?.classList.remove('d-none');
    this.categoryService.getCategories().subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.categories = res.data;
        this.categorieslist = res.data;;
        this.categories = this.categorieslist.slice(0, 15);
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
    var idCategory = this.categoryForm.controls['id'].value;
    if (this.categoryForm.valid && this.checkValidate) {
      this.addCategory?.hide();
      const dataInsert: CategoryModel = {
        id: idCategory,
        name: this.categoryForm.value.name,
        pictureUrl: this.pictureUrl,
        publicId: this.publicId,
      }
      if (idCategory != null && idCategory != 0 && idCategory != "" && idCategory != undefined) {
        this.categoryService.updateInformationCategories(dataInsert).subscribe((res) => {
          if (res.retCode == 0 && res.systemMessage == "") {
            this.loadDataCategory();
            this.categoryForm.reset();
          } else {
            this.messengerService.errorWithIssue();
          }
        });
      } else {
        this.categoryService.createCategories(dataInsert).subscribe((res) => {
          if (res.retCode == 0 && res.systemMessage == "") {
            this.loadDataCategory();
            this.categoryForm.reset();
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
      this.categories = this.categorieslist.filter((el: any) => el.name.toLowerCase().includes(this.term.toLowerCase()))
    } else {
      this.categories = this.categorieslist.slice(0, 15)
    }
    // noResultElement
    this.updateNoResultDisplay();
  }

  // no result 
  updateNoResultDisplay() {
    const noResultElement = document.querySelector('.noresult') as HTMLElement;
    const paginationElement = document.getElementById('pagination-element') as HTMLElement
    if (this.term && this.categories.length === 0) {
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
    this.categories = this.categorieslist.slice(startItem, endItem);
  }

  addCategoryModal() {
    this.categoryForm.reset();
    this.publicId = "";
    this.pictureUrl = "";
    this.addCategory?.show();
    var modaltitle = document.querySelector('.modal-title') as HTMLAreaElement
    modaltitle.innerHTML = 'Thêm loại khoá học'
    var modalButton = document.getElementById('add-btn') as HTMLAreaElement
    modalButton.innerHTML = 'Lưu';
  }

  editCategory(id: number) {
    this.idCategory = id;
    this.addCategory?.show();
    var modaltitle = document.querySelector('.modal-title') as HTMLAreaElement
    modaltitle.innerHTML = 'Chỉnh sửa loại khoá học'
    var modalButton = document.getElementById('add-btn') as HTMLAreaElement
    modalButton.innerHTML = 'Cập nhật';
    var dataEdit = this.categories.find(x => x.id == id);
    this.publicId = dataEdit.publicId;
    this.pictureUrl = dataEdit.pictureUrl;
    this.categoryForm.patchValue(dataEdit);
  }

  deleteCategory() {
    this.categoryService.deleteCategories(this.idCategory).subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "") {
        this.loadDataCategory();
      }
      this.addCategory?.hide();
    })
  }
}
