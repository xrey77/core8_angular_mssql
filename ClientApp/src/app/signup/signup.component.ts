import { Component, OnInit } from '@angular/core';
import { RegistrationService } from '../registration.service';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { faAddressCard } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  registrationData: any;
  registrationMessage: any; 
  firstname: any;
  lastname: any;
  email: any;
  mobile: any;
  username: any;
  password: any;
  addresscard = faAddressCard;
  constructor(
    private registrationService: RegistrationService,
  ) { }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  submitRegistrationForm(registrationForm:NgForm) {
    if(registrationForm.valid)
    {
       this.registrationData = registrationForm.value;

       this.registrationService.sendRegistrationRequest(this.registrationData).subscribe(res => {
        if(res.statuscode == 200) {
          this.registrationMessage = res.message;
          this.firstname = null;
          this.lastname = null;
          this.email = null;
          this.mobile = null;
          this.username = null;
          this.password = null
          return;
        } else {
          this.registrationMessage = res.message;
          setTimeout(() => {
            this.registrationMessage = null;            
          }, 3000);

        }

      });     
    }
  }
}

