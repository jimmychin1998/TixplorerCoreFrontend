import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
      // filenameHashing為vue寫法，此處為vue
    // filenameHashing: false, //避免打包檔案時dist裡面的檔案有雜湊名稱
    //避免打包檔案時dist裡面的檔案有雜湊名稱
    build: {
      rollupOptions: {
        output: {
          entryFileNames: `assets/[name].js`,
          chunkFileNames: `assets/[name].js`,
          assetFileNames: `assets/[name].[ext]`
        },
        // input: {
        //   main: './index.html',
        //   // ...
        //   // List all files you want in your build
        // }
      }
    },
})
