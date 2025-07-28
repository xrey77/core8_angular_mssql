import { Component, NgZone, ChangeDetectorRef, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { ProfileService } from '../profile.service';
// import { ConnectableObservable } from 'rxjs';

declare var $:any;

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit  {

  profileData: any;
  profilePassword: any;
  profileMsg: any;
  userId: any;
  jwttoken: any;
  xrey: any = [];
  mfa: boolean = false;
  firstname: string = '';
  lastname: string = '';
  email: string = '';
  mobile: string = '';
  profilepic: string = '';
  password: string  = '';
  confpassword: string = '';
  qrcodeurl: any = null;
  userpic: string = '';

  constructor(
    private profileService: ProfileService,
    private ngZone: NgZone, 
    private cdRef: ChangeDetectorRef    
  ) 
  {
     this.userId = sessionStorage.getItem("USERID");
     this.jwttoken = sessionStorage.getItem("TOKEN");
     this.profileService.getUserbyId(this.userId, this.jwttoken).subscribe((res: any) => {
         if(res.statuscode == 200) {
            this.firstname = res.user.firstname;
            this.lastname = res.user.lastname;
            this.email = res.user.email;
            this.mobile = res.user.mobile;
            this.profilepic = res.user.profilepic;
            sessionStorage.setItem("USERPIC", res.user.profilepic);
            if (res.user.qrcodeurl != null) {
              const img = new Image();
              img.src = res.user.qrcodeurl;
              this.mfa = true;
              this.qrcodeurl = img.src;
            } else {
              this.mfa = false;
            }
         } else {
           this.profileMsg = res.message;

         }
     });

  }

  ngOnInit(): void {
    $("#cpwd").hide();
    $("#mfa1").hide();
    $("#mfa2").hide();  
  }

  changeProfilepic(event: any) {
    $("#pix").attr('src',URL.createObjectURL(event.target.files[0]));
    
    let formdata = new FormData();
    formdata.append('id', this.userId);
    formdata.append('profilepic',event.target.files[0]);

    this.profileService.UploadPicture(formdata, this.jwttoken).subscribe((res: any) => {
      if(res.statuscode == 200) {
        this.profileMsg = res.message;
        window.setTimeout(() => {
          window.location.reload();
        }, 3000);
      } else {
        this.profileMsg = res.message;
      }
    });    
  }

  changePassword() {
    if ($('#changepwd').is(":checked")) {
      $("#cpwd").show();
      $("#mfa1").hide();  
      $("#mfa2").hide();  
      $('#twofactor').prop('checked', false);
    } else {
      $("#cpwd").hide();
    }            
  }

  onetimePassword() {
    if ($('#twofactor').is(":checked")) {
      $("#cpwd").hide();
      $("#mfa1").show();
      $("#mfa2").show();
      $('#changepwd').prop('checked', false);
    } else {
      $("#mfa1").hide();  
      $("#mfa2").hide();  
    }            
  }

  passwordChange(passwordForm:NgForm) {
    this.ngZone.run(() => {

      if (this.password != this.confpassword) {
        this.profileMsg = "New password does not mactched..."
        window.setTimeout(() => {
          this.profileMsg = null;
          window.location.reload();          
        }, 3000)
        return;
      }
      this.profileData = passwordForm.value;
      this.profileService.sendNewpasswordRequest(this.userId, this.profileData, this.jwttoken).subscribe((res: any) => {
        if(res.statuscode == 200) {
          this.profileMsg = res.message;
          window.setTimeout(() => {
            this.profileMsg  = null;
            window.location.reload();
          }, 3000);

        } else {
          this.profileMsg = res.message;
          window.setTimeout(() => {
            this.profileMsg  = null;
          }, 3000);
        }
    });      



    });
  }

  enableMFA(event: any) {
    event.preventDefault();    
    this.xrey = {
      twofactorenabled: true
    }

    this.profileService.ActivateMFA(this.userId, this.xrey, this.jwttoken).subscribe((res: any) => {
      console.log(res);
      if(res.statuscode == 200) {
        this.profileMsg = res.message;
      } else {
        this.profileMsg = res.message;
      }
      window.setTimeout(() => {
        this.profileMsg = null;
        window.location.reload();
      }, 3000);
  
    });
  }

  disableMFA(event: any) {
    event.preventDefault();      
    console.log("disable....")
    this.xrey = {
      twofactorenabled: false
    }

    this.profileService.ActivateMFA(this.userId, this.xrey, this.jwttoken).subscribe((res: any) => {
      if(res.statuscode == 200) {
        this.profileMsg = res.message;
      } else {
        this.profileMsg = res.message;
      }
    });
    window.setTimeout(() => {
      this.profileMsg = null;
      window.location.reload();
    }, 3000);

  }

  submitProfileForm(profileForm:NgForm) {
    this.ngZone.run(() => {
        if (this.firstname == '' || this.lastname == '' || this.mobile == '') {
          this.profileMsg = "Error! Empty values not allowed..."
          window.setTimeout(() => {
            this.profileMsg  = null;
          }, 3000);
          return;
        }
        this.profileData = profileForm.value;
        this.profileService.sendProfileRequest(this.userId, this.profileData, this.jwttoken).subscribe((res: any) => {
          if(res.statuscode == 200) {
            this.profileMsg = res.message;
            window.setTimeout(() => {
              this.profileMsg  = null;
              window.location.reload();
            }, 3000);

          } else {
            this.profileMsg = res.message;
            window.setTimeout(() => {
              this.profileMsg  = null;
            }, 3000);
          }
      });      
    });
  }  
}
