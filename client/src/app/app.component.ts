import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'The Dating App';
  users: any;

  constructor(private accountSerivce: AccountService) { } //to make http request

  ngOnInit() {
    this.setCurrentUser();
}

  setCurrentUser() {
    let userItem = localStorage.getItem("user")
    if (userItem != null) {
      const user: User = JSON.parse(userItem);
      this.accountSerivce.SetCurrentUser(user);
    }
    
  }

 
}
