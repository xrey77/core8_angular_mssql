import { Component, OnInit } from '@angular/core';
import { LoginService } from '../login.service';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { windowToggle } from 'rxjs';
import { faUnlockKeyhole } from '@fortawesome/free-solid-svg-icons';


declare var $:any;

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit{
  loginMessage: any;  
  loginData: any;
  username: any;
  password: any;
  faunlock = faUnlockKeyhole;
  constructor(
    private loginService: LoginService

  ) { }

  ngOnInit(): void {
  }

  submitLoginForm(loginForm:NgForm) {
    if(loginForm.valid)
    {
       this.loginData = loginForm.value;
       this.loginService.sendLoginRequest(this.loginData).subscribe(res => {
          if(res.statuscode == 200) {
            this.loginMessage = res.message;
            if (res.isactivated == 0) {
              window.setTimeout(() => {
                this.loginMessage  = null;
              }, 3000);
  
              return;
            }
            sessionStorage.setItem("USERID", res.id);
            sessionStorage.setItem("TOKEN", res.token);
            sessionStorage.setItem("USERPIC", res.profilepic);

            if (res.qrcodeurl != null) {
              $("#loginClose").click();
              $("#mfashow").click();
              return;
            }
            sessionStorage.setItem("USERNAME", res.username);
            this.username = null;
            this.password = null;
            window.setTimeout(() => {
              this.loginMessage  = null;
              window.location.reload();
            }, 6000);

          } else {
            this.loginMessage = res.message;
            setTimeout(() => {
              this.loginMessage  = null;
            }, 3000);

          }
      });
    }
  }
}
