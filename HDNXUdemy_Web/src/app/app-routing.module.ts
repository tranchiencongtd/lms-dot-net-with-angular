import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ElearningSchoolComponent } from './components/pages/elearning-school/elearning-school.component';
import { ContactUsComponent } from './components/pages/contact-us/contact-us.component';
import { GalleryComponent } from './components/pages/gallery/gallery.component';
import { AboutComponent } from './components/pages/about/about.component';
import { TeacherComponent } from './components/pages/teacher/teacher.component';
import { ComingSoonComponent } from './components/pages/coming-soon/coming-soon.component';
import { PurchaseGuideComponent } from './components/pages/purchase-guide/purchase-guide.component';
import { PrivacyPolicyComponent } from './components/pages/privacy-policy/privacy-policy.component';
import { TermsOfServiceComponent } from './components/pages/terms-of-service/terms-of-service.component';
import { FaqComponent } from './components/pages/faq/faq.component';
import { CoursesListComponent } from './components/pages/courses-list/courses-list.component';
import { CoursesDetailsComponent } from './components/pages/courses-details/courses-details.component';
import { MembershipLevelsComponent } from './components/pages/membership-levels/membership-levels.component';
import { BecomeATeacherComponent } from './components/pages/become-a-teacher/become-a-teacher.component';
import { CategoriesComponent } from './components/pages/categories/categories.component';
import { DownLoadRecoursesComponent } from './components/pages/down-load-recourses/down-load-resourses.component';
import { ProductsListComponent } from './components/pages/products-list/products-list.component';
import { CartComponent } from './components/pages/cart/cart.component';
import { CheckoutComponent } from './components/pages/checkout/checkout.component';
import { ProductsDetailsComponent } from './components/pages/products-details/products-details.component';
import { CoursesNewsComponent } from './components/pages/courses-news/courses-news.component';
import { BlogDetailsStyleTwoComponent } from './components/pages/courses-details-news/courses-details-news.component';
import { CoursesContentDetailComponent } from './components/pages/courses-content-detail/courses-content-detail.component';
import { LoginPagesComponent } from './components/pages/login-pages/login-pages.component';
import { RegisterPagesComponent } from './components/pages/register-pages/register-pages.component';
import { AccountProfileCoursesComponent } from './components/pages/account-profile-courses/account-profile-courses.component';
import { AccountProfileDetailsComponent } from './components/pages/account-profile-details/account-profile-details.component';
import { TrainingCoursesComponent } from './components/pages/training-courses-list/training-courses-list.component';
import { TrainingCoursesDetailsComponent } from './components/pages/training-courses-details/training-courses-details.component';
import { ProfileTeacherComponent } from './components/pages/profile-teacher/profile-teacher.component';
import { RegisterErrorComponent } from './components/pages/register-error/register-error.component';
import { SendEmailAfterRegisterComponent } from './components/pages/send-email-after-register/send-email-after-register.component';
import { NotificationUsersComponent } from './components/pages/notification-users/notification-users.component';
import { AccountBookmarkCourseComponent } from './components/pages/account-bookmark-course/account-bookmark-course.component';
import { PaymentErrorComponent } from './components/pages/payment-error/payment-error.component';
import { PaymentSuccessComponent } from './components/pages/payment-success/payment-success.component';

const routes: Routes = [
    { path: '', component: ElearningSchoolComponent },
    { path: 'about', component: AboutComponent },
    { path: 'teacher', component: TeacherComponent },
    { path: 'infomation-teacher', component: ProfileTeacherComponent },
    { path: 'faq', component: FaqComponent },
    { path: 'coming-soon', component: ComingSoonComponent },
    { path: 'purchase-guide', component: PurchaseGuideComponent },
    { path: 'privacy-policy', component: PrivacyPolicyComponent },
    { path: 'terms-of-service', component: TermsOfServiceComponent },
    { path: 'courses/:id', component: CoursesListComponent },
    { path: 'detail-courses/:id', component: CoursesDetailsComponent },
    { path: 'membership-levels', component: MembershipLevelsComponent },
    { path: 'become-teacher', component: BecomeATeacherComponent },
    { path: 'categories', component: CategoriesComponent },
    { path: 'download-document', component: DownLoadRecoursesComponent },
    { path: 'products', component: ProductsListComponent },
    { path: 'cart', component: CartComponent },
    { path: 'payment/:purchaseCode', component: CheckoutComponent },
    { path: 'details-products', component: ProductsDetailsComponent },
    { path: 'gallery', component: GalleryComponent },
    { path: 'news', component: CoursesNewsComponent },
    { path: 'detail-news', component: BlogDetailsStyleTwoComponent },
    { path: 'contact', component: ContactUsComponent },
    { path: 'detail-content-course/:id', component: CoursesContentDetailComponent },
    { path: 'login', component: LoginPagesComponent },
    { path: 'register', component: RegisterPagesComponent },
    { path: 'my-course', component: AccountProfileCoursesComponent },
    { path: 'my-information', component: AccountProfileDetailsComponent },
    { path: 'training-online', component: TrainingCoursesComponent },
    { path: 'detail-training-online', component: TrainingCoursesDetailsComponent },
    // Here add new pages component

    { path: 'register-error', component: RegisterErrorComponent },
    { path: 'send-email-register-success', component: SendEmailAfterRegisterComponent },
    { path: 'notification', component: NotificationUsersComponent },
    { path: 'bookmark-course', component: AccountBookmarkCourseComponent },
    { path: 'payment-success', component: PaymentSuccessComponent },
    { path: 'payment-error', component: PaymentErrorComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes, {})],
    exports: [RouterModule]
})
export class AppRoutingModule { }