import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { BlogImage } from './models/image-model';

@Injectable({
  providedIn: 'root'
})
export class ImageService {
  selectedImage : BehaviorSubject<BlogImage> = new BehaviorSubject<BlogImage>({
    id: '',
    fileExtension: '',
    fileName:'',
    title:'',
    url:''
  });

  constructor(private http: HttpClient) { }

  getAllImages(): Observable<BlogImage[]>{
    return this.http.get<BlogImage[]>(`${environment.apiBaseUrl}api/Images`);
  }

  uploadImage(file: File, fileName:string,title:string) : Observable<BlogImage>{
    const formData = new FormData();
    formData.append('file',file);
    formData.append('fileName',fileName);
    formData.append('title',title);

    return this.http.post<BlogImage>(`${environment.apiBaseUrl}api/Images/UploadImage`,formData);
  }
  
  selectImage(image : BlogImage) : void {
    this.selectedImage.next(image);
  }

  onSelectImage(): Observable<BlogImage>{
    return this.selectedImage.asObservable()
  }
}
