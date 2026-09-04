import { createHash } from 'node:crypto';
import path from 'node:path';
import { fileURLToPath } from 'node:url';
import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url)),
    },
  },
  css: {
    modules: {
      generateScopedName(className, filename) {
        const componentName = path.basename(filename).replace('.module.scss', '');
        const hash = createHash('sha256')
          .update(`${filename}:${className}`)
          .digest('base64url')
          .slice(0, 5);

        return `${componentName}__${className}__${hash}`;
      },
    },
  },
});
