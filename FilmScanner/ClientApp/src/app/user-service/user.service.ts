import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({ providedIn: "root" })
export class UserService {
    baseUrl: string = "https://localhost:44380/api/";
    constructor(private http: HttpClient) { }

    //#region Films

    addFilm(externalID: string): Observable<Object> {
        return this.http.post(this.baseUrl + "users/1/films", externalID);
    }

    //#endregion



    //   //#region Users

    //   getAllUsers(): Observable<User[]> {
    //     return this.http.get<User[]>(this.baseUrl + "users");
    //   }

    //   addUser(user: User): Observable<Object> {
    //     return this.http.post(this.baseUrl + "users", user);
    //   }

    //   editUser(userId: number,user: User): Observable<Object> {
    //     return this.http.put(this.baseUrl + `users/${userId}`, user);
    //   }

    //   //#endregion


    //   //#region Follows

    //   getFollowers(userId: number): Observable<User[]> {
    //     return this.http.get<User[]>(this.baseUrl + `users/${userId}/followers`);
    //   } 

    //   getFollowing(userId: number): Observable<User[]> {
    //     return this.http.get<User[]>(this.baseUrl + `users/${userId}/following`);
    //   }

    //   follow(userId: number, userToFollowId: number): Observable<object> {
    //     return this.http.post(
    //       this.baseUrl + `users/${userId}/following/${userToFollowId}`,
    //       null
    //     );
    //   }

    //   unfollow(userId: number, userToUnfollowId: number): Observable<object> {
    //     return this.http.delete(
    //       this.baseUrl + `users/${userId}/following/${userToUnfollowId}`
    //     );
    //   }

    //   explore(userId: number): Observable<User[]> {
    //     return this.http.get<User[]>(this.baseUrl + `users/${userId}/explore`);
    //   }

    //   //#endregion
}