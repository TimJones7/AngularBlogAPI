import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Post } from 'src/app/Models/post.model';
import { PostService } from 'src/app/Services/post.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-admin-posts',
  templateUrl: './admin-posts.component.html',
  styleUrls: ['./admin-posts.component.css']
})
export class AdminPostsComponent implements OnInit {

  constructor(private postService: PostService) { }


  ngOnInit(): void {
    this.postService.getAllPosts()
    .subscribe(
      response =>{
      console.log(response);
      }
    );
  }
}
