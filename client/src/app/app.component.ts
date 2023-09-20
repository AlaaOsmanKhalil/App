import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'The Dating App';
  users: any;

  constructor(private http: HttpClient) { } //to make http request

  ngOnInit() {

    this.getUsers();
}

  getUsers() {
   
    this.http.get('https://localhost:44316/api/users').subscribe(reponse => {
      this.users = reponse;
      console.log('Getting users...'); // newly added
    }, error => {
      console.log(error);
    })
  }
}
