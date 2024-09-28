import { Component, ViewChild } from '@angular/core';
import { DatePipe, DecimalPipe } from '@angular/common';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';

// modal
import { ModalDirective } from 'ngx-bootstrap/modal';
import { DropzoneConfigInterface } from 'ngx-dropzone-wrapper';
import { Store } from '@ngrx/store';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { cloneDeep } from 'lodash';
import { addagentData, deleteagentData, fetchagentData, updateagentData } from 'src/app/store/Agent/agent.action';
import { selectagentData } from 'src/app/store/Agent/agent-selector';
import { addinstructorlistData, deleteinstructorlistData, updateinstructorlistData } from 'src/app/store/Learning-instructor/instructor.action';
import { StudentUser } from 'src/app/models/models/student-user';
import { StudentServices } from 'src/app/core/services/student.service';
import { Router } from '@angular/router';
import { Options } from 'ngx-slider-v2';


@Component({
  selector: 'app-list-user-manager',
  templateUrl: './list-user-manager.component.html',
  styleUrls: ['./list-user-manager.component.scss'],
  providers: [DecimalPipe]
})

// List Component
export class ListUserManagerComponent {
  endItem: any;
  // bread crumb items
  breadCrumbItems!: Array<{}>;
  totalinstructorChart: any;
  totalcoursesChart: any;
  instuctoractivity: any;
  files: File[] = [];
  deleteId: any;
  instructorship: any;
  ListForm!: UntypedFormGroup;
  submitted = false;
  masterSelected!: boolean;
  priceoption: Options = {
    floor: 0,
    ceil: 2000,
    translate: (value: number): string => {
      return '$' + value;
    },
  };

  instructors: any;
  idUser = 0;
  @ViewChild('addInstructor', { static: false }) addInstructor?: ModalDirective;
  @ViewChild('deleteRecordModal', { static: false })
  deleteRecordModal?: ModalDirective;
  editData: any;
  term: any;
  // Price Slider
  pricevalue: number = 100;
  minVal: number = 100;
  maxVal: number = 1000;
  students: StudentUser[] = [];
  listStudent: StudentUser[] = [];
  isLoading = false;

  constructor(
    private formBuilder: UntypedFormBuilder,
    public store: Store,
    private studentServices: StudentServices,
    private router: Router,) { }

  ngOnInit(): void {
    this.breadCrumbItems = [
      { label: 'Danh sách người dùng', active: true },
    ];

    // Chart Color Data Get Function
    this._totalinstructorChart('["--tb-primary"]');
    this._totalcoursesChart('["--tb-secondary"]');
    this._instuctoractivity('["--tb-primary", "--tb-light", "--tb-secondary"]');

    this.ListForm = this.formBuilder.group({
      id: [''],
      name: ['', [Validators.required]],
      email: ['', [Validators.required]],
      total_course: ['', [Validators.required]],
      experience: ['', [Validators.required]],
      students: ['', [Validators.required]],
      contact: ['', [Validators.required]],
      status: ['', [Validators.required]],
      img: [''],
    });
    this.loadDataStudent();
  }

  // File Upload
  public dropzoneConfig: DropzoneConfigInterface = {
    clickable: true,
    addRemoveLinks: true,
    previewsContainer: false,
  };

  uploadedFiles: any[] = [];

  // File Upload
  imageURL: any;
  onUploadSuccess(event: any) {
    setTimeout(() => {
      this.uploadedFiles.push(event[0]);
      this.ListForm.controls['img'].setValue(event[0].dataURL);
    }, 0);
  }

  // File Remove
  removeFile(event: any) {
    this.uploadedFiles.splice(this.uploadedFiles.indexOf(event), 1);
  }

  // Chart Colors Set
  private getChartColorsArray(colors: any) {
    colors = JSON.parse(colors);
    return colors.map(function (value: any) {
      var newValue = value.replace(' ', '');
      if (newValue.indexOf(',') === -1) {
        var color = getComputedStyle(document.documentElement).getPropertyValue(
          newValue
        );
        if (color) {
          color = color.replace(' ', '');
          return color;
        } else return newValue;
      } else {
        var val = value.split(',');
        if (val.length == 2) {
          var rgbaColor = getComputedStyle(
            document.documentElement
          ).getPropertyValue(val[0]);
          rgbaColor = 'rgba(' + rgbaColor + ',' + val[1] + ')';
          return rgbaColor;
        } else {
          return newValue;
        }
      }
    });
  }

  /**
   * Total Instructors Charts
   */
  private _totalinstructorChart(colors: any) {
    colors = this.getChartColorsArray(colors);
    this.totalinstructorChart = {
      series: [84],
      chart: {
        height: 170,
        type: 'radialBar',
        sparkline: {
          enabled: true,
        },
      },
      plotOptions: {
        radialBar: {
          hollow: {
            margin: 0,
            size: '75%',
          },
          track: {
            margin: 0,
          },
          dataLabels: {
            show: false,
          },
        },
      },
      stroke: {
        lineCap: 'round',
      },
      labels: ['Instructor Total'],
      colors: colors,
    };

    const attributeToMonitor = 'data-theme';

    const observer = new MutationObserver(() => {
      this._totalinstructorChart('["--tb-primary"]');
    });
    observer.observe(document.documentElement, {
      attributes: true,
      attributeFilter: [attributeToMonitor],
    });
  }

  /**
   * Total Course Charts
   */
  private _totalcoursesChart(colors: any) {
    colors = this.getChartColorsArray(colors);
    this.totalcoursesChart = {
      series: [33],
      chart: {
        height: 170,
        type: 'radialBar',
        sparkline: {
          enabled: true,
        },
      },
      plotOptions: {
        radialBar: {
          hollow: {
            margin: 0,
            size: '75%',
          },
          track: {
            margin: 0,
          },
          dataLabels: {
            show: false,
          },
        },
      },
      stroke: {
        lineCap: 'round',
      },
      labels: ['Instructor Total'],
      colors: colors,
    };

    const attributeToMonitor = 'data-theme';

    const observer = new MutationObserver(() => {
      this._totalcoursesChart('["--tb-secondary"]');
    });
    observer.observe(document.documentElement, {
      attributes: true,
      attributeFilter: [attributeToMonitor],
    });
  }

  /**
   * Instructor Activity Charts
   */
  private _instuctoractivity(colors: any) {
    colors = this.getChartColorsArray(colors);
    this.instuctoractivity = {
      series: [
        {
          name: 'New Orders',
          data: [
            32, 18, 13, 17, 26, 34, 47, 51, 59, 63, 44, 38, 53, 69, 72, 83, 90,
            110, 130, 117, 103, 92, 95, 119, 80, 96, 116, 125,
          ],
        },
        {
          name: 'Return Orders',
          data: [
            3, 6, 2, 4, 7, 9, 15, 10, 19, 22, 27, 21, 34, 23, 29, 32, 41, 34,
            29, 37, 70, 55, 49, 36, 30, 52, 38, 33,
          ],
        },
      ],
      chart: {
        height: 190,
        type: 'line',
        toolbar: {
          show: false,
        },
      },
      legend: {
        show: false,
        position: 'top',
        horizontalAlign: 'right',
      },
      grid: {
        yaxis: {
          lines: {
            show: false,
          },
        },
      },
      markers: {
        size: 0,
        hover: {
          sizeOffset: 4,
        },
      },
      stroke: {
        curve: 'smooth',
        width: 2,
      },
      colors: colors,
      xaxis: {
        type: 'datetime',
        categories: [
          '02/01/2023 GMT',
          '02/02/2023 GMT',
          '02/03/2023 GMT',
          '02/04/2023 GMT',
          '02/05/2023 GMT',
          '02/06/2023 GMT',
          '02/07/2023 GMT',
          '02/08/2023 GMT',
          '02/09/2023 GMT',
          '02/10/2023 GMT',
          '02/11/2023 GMT',
          '02/12/2023 GMT',
          '02/13/2023 GMT',
          '02/14/2023 GMT',
          '02/15/2023 GMT',
          '02/16/2023 GMT',
          '02/17/2023 GMT',
          '02/18/2023 GMT',
          '02/19/2023 GMT',
          '02/20/2023 GMT',
          '02/21/2023 GMT',
          '02/22/2023 GMT',
          '02/23/2023 GMT',
          '02/24/2023 GMT',
          '02/25/2023 GMT',
          '02/26/2023 GMT',
          '02/27/2023 GMT',
          '02/28/2023 GMT',
        ],
      },
      yaxis: {
        show: false,
      },
    };

    const attributeToMonitor = 'data-theme';

    const observer = new MutationObserver(() => {
      this._instuctoractivity(
        '["--tb-primary", "--tb-light", "--tb-secondary"]'
      );
    });
    observer.observe(document.documentElement, {
      attributes: true,
      attributeFilter: [attributeToMonitor],
    });
  }

  // Edit Data
  editList(id: any) {
    this.uploadedFiles = [];
    this.addInstructor?.show();
    var modaltitle = document.querySelector('.modal-title') as HTMLAreaElement;
    modaltitle.innerHTML = 'Edit Product';
    var modal = document.getElementById('add-btn') as HTMLAreaElement;
    modal.innerHTML = 'Update';

    this.editData = this.instructors[id];

    this.uploadedFiles.push({
      dataURL: this.editData.img,
      name: this.editData.img_alt,
      size: 1024,
    });

    this.ListForm.patchValue(this.instructors[id]);
  }

  // add Product
  saveList() {
    if (this.ListForm.valid) {
      if (this.ListForm.get('id')?.value) {
        const updatedData = {
          rating: this.editData.rating,
          ...this.ListForm.value,
        };
        this.store.dispatch(updateinstructorlistData({ updatedData }));
      } else {
        this.ListForm.controls['id'].setValue(this.instructors.length + 1);
        const newData = {
          rating: '4.3',
          ...this.ListForm.value,
        };
        this.store.dispatch(addinstructorlistData({ newData }));
      }
      this.addInstructor?.hide();

      setTimeout(() => {
        this.ListForm.reset();
      }, 2000);
      this.uploadedFiles = [];
      this.ListForm.reset();
    }
  }

  checkedValGet: any[] = [];
  // The master checkbox will check/ uncheck all items
  checkUncheckAll(ev: any) {
    this.instructors.forEach(
      (x: { state: any }) => (x.state = ev.target.checked)
    );
    var checkedVal: any[] = [];
    var result;
    for (var i = 0; i < this.instructors.length; i++) {
      if (this.instructors[i].state == true) {
        result = this.instructors[i].id;
        checkedVal.push(result);
      }
    }
    this.checkedValGet = checkedVal;
    checkedVal.length > 0
      ? document.getElementById('remove-actions')?.classList.remove('d-none')
      : document.getElementById('remove-actions')?.classList.add('d-none');
  }

  // Select Checkbox value Get
  onCheckboxChange(e: any) {
    var checkedVal: any[] = [];
    var result;
    for (var i = 0; i < this.instructors.length; i++) {
      if (this.instructors[i].state == true) {
        result = this.instructors[i].id;
        checkedVal.push(result);
      }
    }
    this.checkedValGet = checkedVal;
    checkedVal.length > 0
      ? document.getElementById('remove-actions')?.classList.remove('d-none')
      : document.getElementById('remove-actions')?.classList.add('d-none');
  }

  // Delete Product
  removeItem(id: any) {
    this.deleteId = id;
    this.deleteRecordModal?.show();
  }

  confirmDelete(id: any) {
    this.deleteRecordModal?.hide();
    if (id) {
      this.store.dispatch(
        deleteinstructorlistData({ id: this.deleteId.toString() })
      );
    }
    this.store.dispatch(
      deleteinstructorlistData({ id: this.checkedValGet.toString() })
    );
    this.deleteRecordModal?.hide();
    this.masterSelected = false;
  }

  // Sort Data
  direction: any = 'asc';
  onSort(column: any) {
    if (this.direction == 'asc') {
      this.direction = 'desc';
    } else {
      this.direction = 'asc';
    }
    const sortedArray = [...this.instructors]; // Create a new array
    sortedArray.sort((a, b) => {
      const res = this.compare(a[column], b[column]);
      return this.direction === 'asc' ? res : -res;
    });
    this.instructors = sortedArray;
  }
  compare(v1: string | number, v2: string | number) {
    return v1 < v2 ? -1 : v1 > v2 ? 1 : 0;
  }

  // paperhanger
  pageChanged(event: any) {
    const startItem = (event.page - 1) * event.itemsPerPage;
    this.endItem = event.page * event.itemsPerPage;
    this.instructors = this.instructorship.slice(startItem, this.endItem);
  }

  // filterdata
  filterdata() {
    if (this.term) {
      this.instructors = this.instructorship.filter((el: any) =>
        el.name.toLowerCase().includes(this.term.toLowerCase())
      );
    } else {
      this.instructors = this.instructorship.slice(0, 10);
    }
    // noResultElement
    this.updateNoResultDisplay();
  }

  // no result
  updateNoResultDisplay() {
    const noResultElement = document.querySelector('.noresult') as HTMLElement;
    const paginationElement = document.getElementById(
      'pagination-element'
    ) as HTMLElement;
    if (this.term && this.instructors.length === 0) {
      noResultElement.style.display = 'block';
      paginationElement.classList.add('d-none');
    } else {
      noResultElement.style.display = 'none';
      paginationElement.classList.remove('d-none');
    }
  }

  //  Filter Offcanvas Set
  openEnd() {
    document.getElementById('courseFilters')?.classList.add('show')
    document.querySelector('.backdrop3')?.classList.add('show')
  }
  closeoffcanvas() {
    document.getElementById('courseFilters')?.classList.remove('show')
    document.querySelector('.backdrop3')?.classList.remove('show')
  }

  /**
* Range Slider Wise Data Filter
*/
  valueChange(value: number, boundary: boolean): void {
    if (boundary) {
      this.minVal = value;
    } else {
      this.maxVal = value;
    }
  }

  loadDataStudent() {
    this.isLoading = true;
    this.studentServices.getUserOfProject().subscribe((res) => {
      if (res.retCode === 0 || res.systemMessage === '') {
        this.students = res.data;
        console.log(this.students);
        this.listStudent = res.data;
        this.isLoading = false;
      } else {
        this.isLoading = false;
      }
    });
  }

  addEditStudent(id: number) {
    this.router.navigate([`/user-manager/add-edit-user-account/${id}`]);
  }
}
