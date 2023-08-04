import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'
import './assets/main.css'

// import 'vuetify/dist/vuetify.min.css'
// import { createVuetify } from 'vuetify'
// import * as components from 'vuetify/components'
// import * as directives from 'vuetify/directives'
// import { VDatePicker } from 'vuetify/labs/VDatePicker';
import '@mdi/font/css/materialdesignicons.css';
import { pl, zhHans } from 'vuetify/locale'
// const vuetify = createVuetify({
//     components,
//     components:{
//         VDatePicker
//     },
//     directives,
// })
const app = createApp(App)
app.use(createPinia())
app.use(router)
// app.use(vuetify)
app.mount('#app')
