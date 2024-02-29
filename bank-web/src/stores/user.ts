import { defineStore } from 'pinia'
import axios from 'axios'


const BASE_URL = 'https://localhost:7284/api/User';

export const useUserStore = defineStore('user', {
    state: () => ({
        userId: "",
        isLogin: false
    }),
    actions: {
        async onLogin (data: { username: string; password: string; }) {
            try{
                this.isLogin = false
                const response = await axios.post(`${BASE_URL}/Login`, data)
                console.log('response: ', response)

                localStorage.setItem("token", response.data.token);
                
                localStorage.setItem("userId", response.data.user.userId);

                if(response.data.token !== undefined ){
                    this.isLogin = true
                    this.userId = response.data.user.userId
                }
            }
            catch (error){
                console.log('error: ', error)
            }
        },

        async onLogout () {
            try{
                localStorage.removeItem("token")
                this.isLogin = false
            }
            catch (error){
                console.log('error: ', error)
            }
        }
    }
})

// const DownloadFile = async (uri: string) => {
//     await axios.get(serviceUrl.value + '/' + uri, {
//         headers: {
//             "charset": 'utf-8',
//             "Authorization": "Bearer " + localStorage.getItem('AccessKey'),
//         },
//         'responseType': 'blob'
//     }).then(response => {
//         const url = window.URL.createObjectURL(new Blob([response.data], { type: 'application/pdf' }));
//         window.open(url, '_blank');
//     });
// }
