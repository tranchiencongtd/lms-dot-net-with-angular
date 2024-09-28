import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { MessengerServices } from 'src/app/core/services/messenger.service';
import { StudentServices } from 'src/app/core/services/student.service';
import { Messenger } from 'src/app/models/contants/ennum_router';
import { ERoles } from 'src/app/models/enum/etype_project.enum';
import { StudentUser } from 'src/app/models/models/student-user';


@Component({
  selector: 'app-add-edit-account-user',
  templateUrl: './add-edit-account-user.component.html',
  styleUrls: ['./add-edit-account-user.component.scss']
})
export class AddEditAccountUserComponent implements OnInit {

  // bread crumb items
  breadCrumbItems!: Array<{}>;
  fieldTextType!: boolean;
  fieldTextType1!: boolean;
  fieldTextType2!: boolean;
  bsConfig?: Partial<BsDatepickerConfig>;

  informationForm!: UntypedFormGroup;
  educationForm!: UntypedFormGroup;
  currentTab = 'personalDetails';
  selectedAccount = 'This is a placeholder';
  generals = [
    { id: 0, name: 'Nam' },
    { id: 1, name: 'Nữ' },
  ];
  roles = [
    { id: ERoles.Student, name: 'Học viên' },
    { id: ERoles.Teacher, name: 'Giáo viên' },
    { id: ERoles.Admin, name: 'Admin' },
    { id: ERoles.User, name: 'Người dùng' },
  ];
  idUser = 0;
  isLoading = false;
  student: StudentUser = {
    password: '',
  }

  constructor(
    private formBuilder: FormBuilder,
    private studentUserServices: StudentServices,
    private routers: ActivatedRoute,
    private readonly messengerService: MessengerServices,) { }

  ngOnInit(): void {
    this.idUser = Number(this.routers.snapshot.paramMap.get('id'));
    this.breadCrumbItems = [
      { label: 'Danh sách người dùng', active: true, linkRouter: '/learning/list-student', isRouter: true },
      { label: 'Cài đặt thông tin người dùng', active: true, linkRouter: '', isRouter: false }
    ];

    this.informationForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: [''],
      phone: ['', [Validators.required]],
      general: [''],
      roleId: ['', Validators.required],
    });

    this.loadStudentAndUser(this.idUser);

  }

  // Change Tab Content
  changeTab(tab: string) {
    this.currentTab = tab;
  }

  // File Upload
  imageURL: any;
  fileChange(event: any, id: any) {
    let fileList: any = (event.target as HTMLInputElement);
    let file: File = fileList.files[0];
    const reader = new FileReader();
    reader.onload = () => {
      this.imageURL = reader.result as string;
      if (id == '0') {
        document.querySelectorAll('#cover-img').forEach((element: any) => {
          element.src = this.imageURL;
        });
      }
      if (id == '1') {
        document.querySelectorAll('#user-img').forEach((element: any) => {
          element.src = this.imageURL;
        });
      }
    }

    reader.readAsDataURL(file)
  }

  /**
  * Password Hide/Show
  */
  toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }
  toggleFieldTextType1() {
    this.fieldTextType1 = !this.fieldTextType1
  }
  toggleFieldTextType2() {
    this.fieldTextType2 = !this.fieldTextType2;
  }

  // add Form
  addForm() {
    const formGroupClone = this.formBuilder.group(this.educationForm.value);
  }

  // Delete Form
  deleteForm(id: any) {
  }

  saveUserInformation() {
    this.isLoading = true;
    if (this.informationForm.valid) {
      const dataInsert: StudentUser = {
        name: this.informationForm.value.name,
        email: this.informationForm.value.email,
        password: '',
        phone: this.informationForm.value.phone,
        general: this.informationForm.value.general,
        pictureUrl: '',
        publicId: '',
        roleId: this.informationForm.value.roleId,
        typeLogin: 0,
        isRequestTeacher: this.informationForm.value.roleId === ERoles.Teacher,
        totalCourseRegister: 0,
        status: 0
      }
      if (this.idUser === 0) {
        this.studentUserServices.createStudent(dataInsert).subscribe((res) => {
          if (res.retCode == 0 && res.systemMessage == "" && res.data == true) {
            this.isLoading = false;
            this.messengerService.successes(Messenger.createDataSuccessFull);
            this.informationForm.reset();
          } else {
            this.isLoading = false;
            this.messengerService.errorWithIssue();
          }
        });
      } else {
        this.isLoading = true;
        dataInsert.id = this.student.id;
        dataInsert.password = this.student.password;
        dataInsert.typeLogin = this.student.typeLogin;
        dataInsert.totalCourseRegister = this.student.totalCourseRegister;
        dataInsert.status = this.student.status;
        this.studentUserServices.updateStudent(dataInsert).subscribe((res) => {
          if (res.retCode == 0 && res.systemMessage == "" && res.data == true) {
            this.isLoading = false;
            this.messengerService.successes(Messenger.updateSuccessFull);
          } else {
            this.isLoading = false;
            this.messengerService.errorWithIssue();
          }
        })
      }

    } else {
      this.isLoading = false;
    }

  }

  loadStudentAndUser(id: number) {
    this.isLoading = true;
    this.studentUserServices.getStudent(id).subscribe((res) => {
      if (res.retCode === 0 && res.systemMessage === '') {
        this.student = res.data;
        this.isLoading = false;
        this.informationForm.patchValue(this.student);
      } else {
        this.isLoading = false;
        this.messengerService.errorWithIssue();
      }
    });
  }

  resetInput() {
    this.informationForm.reset();
  }

  updatePassword() {
    this.isLoading = true;
    this.studentUserServices.updateStudent(this.student).subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == "" && res.data == true) {
        this.isLoading = false;
        this.messengerService.successes(Messenger.updateSuccessFull);
      } else {
        this.isLoading = false;
        this.messengerService.errorWithIssue();
      }
    })
  }

}
