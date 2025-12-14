import {createRoot} from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import {BrowserRouter} from "react-router";
import {Provider} from "react-redux";
import {store} from "./store";
import {ThemeProvider} from "./admin/context/ThemeContext.tsx";
import {AppWrapper} from "./admin/components/common/PageMeta.tsx";

createRoot(document.getElementById('root')!).render(
    <ThemeProvider>
        <AppWrapper>
            <Provider store={store}>
                <BrowserRouter>
                    <App/>
                </BrowserRouter>
            </Provider>
        </AppWrapper>
    </ThemeProvider>,
)
