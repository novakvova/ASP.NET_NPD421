import './App.css'
import CategoriesListPage from "./pages/categories/CategoriesListPage";
import CategoryCreatePage from "./pages/categories/CategoryCreatePage";
import {Route, Routes} from "react-router";
import MainLayout from "./components/MainLayout";
import LoginPage from "./pages/account/LoginPage";

function App() {

    return (
        <Routes>
            <Route path={"/"} element={<MainLayout/>}>
                <Route index element={<CategoriesListPage/>}/>
                <Route path={"create"} element={<CategoryCreatePage/>}/>
                <Route path={"login"} element={<LoginPage />}/>
            </Route>
        </Routes>
    )
}

export default App
