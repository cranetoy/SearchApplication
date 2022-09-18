import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  title = 'SearchApplication';

  baseUrl = '/search?searchKey=';
  searchString: any;
  public searchResults?: SearchResult[];

  _http: HttpClient

  constructor(http: HttpClient) {

    this._http = http;
    
  }

  getResults(): any {

    var s = this.searchString;
    var url = this.baseUrl.concat(s);

    this._http.get<SearchResult[]>(url).subscribe(result => {
      this.searchResults = result;
      var x = this.searchResults[0];
      var y = x.name;
      var z = x.description;

    }, error => console.error(error));

  }
}

interface SearchResult {
  name: string;
  entityType: string;
  description: string;
}
