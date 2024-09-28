import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA, DEFAULT_CURRENCY_CODE } from '@angular/core';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CountUpModule } from 'ngx-countup';
import { NgxScrollTopModule } from 'ngx-scrolltop';
import { LightboxModule } from 'ngx-lightbox';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LightgalleryModule } from 'lightgallery/angular';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/common/navbar/navbar.component';
import { FooterComponent } from './components/common/footer/footer.component';
import { ElearningSchoolComponent } from './components/pages/elearning-school/elearning-school.component';
import { PartnerComponent } from './components/common/partner/partner.component';
import { PremiumAccessComponent } from './components/common/premium-access/premium-access.component';
import { SubscribeComponent } from './components/common/subscribe/subscribe.component';
import { ContactUsComponent } from './components/pages/contact-us/contact-us.component';
import { GalleryComponent } from './components/pages/gallery/gallery.component';
import { TeacherComponent } from './components/pages/teacher/teacher.component';
import { PaymentErrorComponent } from './components/pages/payment-error/payment-error.component';
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
import { ElearningBannerComponent } from './components/pages/elearning-school/elearning-banner/elearning-banner.component';
import { ElearningAboutComponent } from './components/pages/elearning-school/elearning-about/elearning-about.component';
import { FunfactsFeedbackComponent } from './components/common/funfacts-feedback/funfacts-feedback.component';
import { VideoComponent } from './components/common/video/video.component';
import { CoursesComponent } from './components/common/courses/courses.component';
import { InstructorComponent } from './components/common/instructor/instructor.component';
import { TrainingComponent } from './components/common/training/training.component';
import { ApplyInstructorComponent } from './components/common/apply-instructor/apply-instructor.component';
import { TeacherRegisterComponent } from './components/common/teacher-register/teacher-register.component';
import { VideoStyleTwoComponent } from './components/common/video-style-two/video-style-two.component';
import { CoursesContentDetailComponent } from './components/pages/courses-content-detail/courses-content-detail.component';
import { LoginPagesComponent } from './components/pages/login-pages/login-pages.component';
import { RegisterPagesComponent } from './components/pages/register-pages/register-pages.component';
import { NgxSimpleCountdownModule } from 'ngx-simple-countdown';
import { AccountProfileCoursesComponent } from './components/pages/account-profile-courses/account-profile-courses.component';
import { AccountProfileDetailsComponent } from './components/pages/account-profile-details/account-profile-details.component';
import { TrainingCoursesComponent } from './components/pages/training-courses-list/training-courses-list.component';
import { TrainingCoursesDetailsComponent } from './components/pages/training-courses-details/training-courses-details.component';
import { AboutComponent } from './components/pages/about/about.component';
import { FeaturesComponent } from './components/common/features/features.component';
import { ProfileTeacherComponent } from './components/pages/profile-teacher/profile-teacher.component';
import { UserCommentCoursesComponent } from './components/dialog/user-comment-courses/user-comment-courses.component';
import { UserReviewCoursesComponent } from './components/dialog/user-review-courses/user-review-courses.component';
import { NgbAccordionModule, NgbModalModule, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxStarsModule } from 'ngx-stars';
import { TransferHttp } from './core/transfer-http/transfer-http';
import { HttpClientModule } from '@angular/common/http';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { HdnxLoadingComponent } from './components/common/hdnx-loading/hdnx-loading.component';
import { RegisterErrorComponent } from './components/pages/register-error/register-error.component';
import { FacebookLoginProvider, GoogleLoginProvider, GoogleSigninButtonModule, SocialAuthServiceConfig, SocialLoginModule } from '@abacritt/angularx-social-login';
import { environment } from 'src/environments/environment';
import { SendEmailAfterRegisterComponent } from './components/pages/send-email-after-register/send-email-after-register.component';
import { NotificationUsersComponent } from './components/pages/notification-users/notification-users.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { NgIconsModule } from '@ng-icons/core';
import { bootstrapBookmark, bootstrapBookmarkCheckFill } from '@ng-icons/bootstrap-icons';
import { AccountBookmarkCourseComponent } from './components/pages/account-bookmark-course/account-bookmark-course.component';
import { MentionModule } from 'angular-mentions';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FooterComponent,
    ElearningSchoolComponent,
    PartnerComponent,
    PremiumAccessComponent,
    SubscribeComponent,
    ContactUsComponent,
    GalleryComponent,
    AboutComponent,
    TeacherComponent,
    PaymentErrorComponent,
    ComingSoonComponent,
    PurchaseGuideComponent,
    PrivacyPolicyComponent,
    TermsOfServiceComponent,
    FaqComponent,
    CoursesListComponent,
    CoursesDetailsComponent,
    MembershipLevelsComponent,
    BecomeATeacherComponent,
    CategoriesComponent,
    DownLoadRecoursesComponent,
    ProductsListComponent,
    CartComponent,
    CheckoutComponent,
    ProductsDetailsComponent,
    CoursesNewsComponent,
    BlogDetailsStyleTwoComponent,
    ElearningBannerComponent,
    FeaturesComponent,
    ElearningAboutComponent,
    FunfactsFeedbackComponent,
    VideoComponent,
    CoursesComponent,
    InstructorComponent,
    TrainingComponent,
    ApplyInstructorComponent,
    TeacherRegisterComponent,
    VideoStyleTwoComponent,
    CoursesContentDetailComponent,
    LoginPagesComponent,
    RegisterPagesComponent,
    AccountProfileCoursesComponent,
    AccountProfileDetailsComponent,
    TrainingCoursesComponent,
    TrainingCoursesDetailsComponent,
    ProfileTeacherComponent,
    UserCommentCoursesComponent,
    UserReviewCoursesComponent,
    HdnxLoadingComponent,
    RegisterErrorComponent,
    SendEmailAfterRegisterComponent,
    NotificationUsersComponent,
    AccountBookmarkCourseComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CarouselModule,
    BrowserAnimationsModule,
    CountUpModule,
    NgxScrollTopModule,
    LightboxModule,
    FormsModule,
    LightgalleryModule,
    NgxSimpleCountdownModule,
    NgbModalModule,
    NgxStarsModule,
    NgbAccordionModule,
    NgbTooltipModule,
    HttpClientModule,
    PaginationModule.forRoot(),
    BsDropdownModule.forRoot(),
    ReactiveFormsModule,
    SocialLoginModule,
    GoogleSigninButtonModule,
    NgIconsModule.withIcons({ bootstrapBookmark, bootstrapBookmarkCheckFill }),
    MentionModule
  ],
  providers: [TransferHttp, {
    provide: 'SocialAuthServiceConfig', useValue: {
      autoLogin: false,
      providers: [
        {
          id: GoogleLoginProvider.PROVIDER_ID,
          provider: new GoogleLoginProvider(environment.clientIdGoogle, {
            oneTapEnabled: true
          }),

        }, {
          id: FacebookLoginProvider.PROVIDER_ID,
          provider: new FacebookLoginProvider(environment.facebookId)
        }
      ], onError: (error) => {
        console.log(error);
      }
    } as SocialAuthServiceConfig
  }, {provide: DEFAULT_CURRENCY_CODE, useValue: 'USD'},],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],

})
export class AppModule { }