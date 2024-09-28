import { Component, OnInit, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationServices } from 'src/app/core/services/authentication.service';
import { CartServices } from 'src/app/core/services/cart.service';
import { CategoryServices } from 'src/app/core/services/category.service';
import { LocalStorageConfig } from 'src/app/library/clientconfig/localstorageconfig';
import { CategoryModel } from 'src/app/models/models/category';
import { StudentUser } from 'src/app/models/models/student-user';
import { LoginRegister } from 'src/app/models/respone_model/login-register-respone';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

    // Navbar Sticky
    isSticky: boolean = false;
    categories: CategoryModel[] = [];
    userStudent: LoginRegister | undefined | null = null;
    public count: number = 0;
    @HostListener('window:scroll', ['$event'])
    checkScroll() {
        const scrollPosition = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0;
        if (scrollPosition >= 50) {
            this.isSticky = true;
        } else {
            this.isSticky = false;
        }
    }
    constructor(
        private readonly categoryServices: CategoryServices,
        private readonly router: Router,
        private readonly cartServices: CartServices,
        private readonly authenticationServices: AuthenticationServices,) { }

    ngOnInit(): void {
        this.authenticationServices.getDataUploadAfterLogin().subscribe((res) => {
            this.userStudent = res;
        })
        this.getInformationOfUser();
        this.loadDataCategory();
        this.count = this.cartServices.count;
        this.cartServices.cartUpdates$.subscribe(() => {
            this.count = this.cartServices.count;
        })
    }

    classApplied = false;
    toggleClass() {
        this.classApplied = !this.classApplied;
    }

    get isMemberAccount(){
        return this.authenticationServices.isCheckMemberAccount();
    }

    userOptionLinkHTML(obj: any): string {
        return obj.icon + '<span class="ms-2">' + obj.name + '</span>';
    }


    logout() {
        // this.loginService.userLogout();
    }

    loadDataCategory() {
        this.categoryServices.getCategories().subscribe((res) => {
            if (res.retCode === 0 && res.systemMessage === '') {
                this.categories = res.data;
            }
        })
    }

    goToViewDetailCourseOfCategory(id: number) {
        this.router.navigate([`/courses/${id}`]);
    }

    goToNotification() {
        this.router.navigate(['/notification'])
    }

    getInformationOfUser() {
        this.userStudent = LocalStorageConfig.GetUser();
    }

    logoutAccount(){
        this.authenticationServices.logoutWithAccount();
        this.router.navigate([`/login`]);
    }

}