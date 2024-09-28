import { FacebookLoginProvider, SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationServices } from 'src/app/core/services/authentication.service';
import { MessengerServices } from 'src/app/core/services/messenger.service';
import { LocalStorageConfig } from 'src/app/library/clientconfig/localstorageconfig';

@Component({
  selector: 'app-login-pages',
  templateUrl: './login-pages.component.html',
  styleUrls: ['./login-pages.component.scss']
})
export class LoginPagesComponent implements OnInit {

  loginForm!: UntypedFormGroup;
  isLoading = false;
  constructor(
    private readonly authenticationService: AuthenticationServices,
    private readonly externalAuthService: SocialAuthService,
    private readonly router: Router,
    private readonly messengerServices: MessengerServices,
    private formBuilder: FormBuilder,
  ) {

    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.externalAuthService.authState.subscribe((user: SocialUser) => {
      if (user.id && user.idToken) {
        this.loginWithGoogle(user);
      }
    });
  }

  loginWithGoogle(model: SocialUser) {
    this.authenticationService.loginWithGoogle(model).subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == '') {
        LocalStorageConfig.SetUser(res.data);
        this.authenticationService.updateAfterLogin(res.data);
        this.router.navigate([`/`]);
        this.isLoading = false;
      } else {
        this.isLoading = false;
        this.messengerServices.errorWithIssue();
      }
    });
  }

  loginWithFaceBook() {
    this.isLoading = true;
    this.externalAuthService.signIn(FacebookLoginProvider.PROVIDER_ID).then((res: SocialUser) => {
      if (res.authToken && res.id) {
        this.authenticationService.loginWithFaceBook(res).subscribe((res) => {
          if (res.retCode == 0 && res.systemMessage == '') {
            this.isLoading = false;
            LocalStorageConfig.SetUser(res.data);
            this.authenticationService.updateAfterLogin(res.data);
            this.router.navigate([`/`]);
          } else {
            this.isLoading = false;
            this.messengerServices.errorWithIssue();
          }
        });
      }
    });
  }

  loginWithEmail() {
    this.isLoading = true;
    this.authenticationService.loginNormalAccount(this.loginForm.value.email, this.loginForm.value.password).subscribe((res) => {
      if (res.retCode == 0 && res.systemMessage == '') {
        this.isLoading = false;
        LocalStorageConfig.SetUser(res.data);
        this.authenticationService.updateAfterLogin(res.data);
        this.router.navigate([`/`]);
      } else {
        
        this.messengerServices.errorWithIssue();
        this.isLoading = false;
      }
    })
  }

}
