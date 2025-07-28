import { Component, OnInit } from '@angular/core';
import { faCircleInfo } from '@fortawesome/free-solid-svg-icons';
import { faBuildingColumns } from '@fortawesome/free-solid-svg-icons';
import { faContactCard} from '@fortawesome/free-solid-svg-icons';
import { faLockOpen } from '@fortawesome/free-solid-svg-icons';
import { faSignOut } from '@fortawesome/free-solid-svg-icons';
import { faAddressCard } from '@fortawesome/free-solid-svg-icons';
import { faInfo } from '@fortawesome/free-solid-svg-icons';
import { faMessage } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  
  isExpanded = false;
  userName: any;
  profilepic: any;
  about = faCircleInfo;
  bank = faBuildingColumns;
  contactus = faContactCard;
  login = faLockOpen;
  logout = faSignOut;
  register = faAddressCard;
  profile = faInfo;
  msgr = faMessage;

  constructor(

    ) { 
      this.userName = sessionStorage.getItem("USERNAME");
      this.profilepic = sessionStorage.getItem('USERPIC');
    }
  
    ngOnInit(): void {
    }
  
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logOut(){
    sessionStorage.removeItem("USERNAME");
    sessionStorage.clear();
    window.location.reload();
  }

}
