/* eslint-disable */
import axios from 'axios'
import Mgr from './SecurityService'
import 'babel-polyfill';

const baseUrl = 'https://localhost:9001/api/';
var user = new Mgr()

export default class userService {

    async defineHeaderAxios() {
        await user.getAcessToken().then(
            acessToken => {
                axios.defaults.headers.common['Authorization'] = 'Bearer ' + acessToken
            }, err => {
                console.log(err)
            })
    }

    async createUser(data) {
        await this.defineHeaderAxios()
        return new Promise((resolve, reject) => {
            axios
                .post(baseUrl + "user/create", data, {
                    // headers: { 'content-type': 'application/form-data' },
                })
                .then(response => resolve(response.data))
                .catch(err => {
                    console.log(err);
                })
        })
    }


}