import './App.css'
import CategoriesListPage from "./pages/categories/CategoriesListPage";
import CategoryCreatePage from "./pages/categories/CategoryCreatePage";
import {Link, Route, Routes} from "react-router";

function App() {

    return (
        <div className={"p-5 bg-white dark:bg-gray-900 antialiased  min-h-screen"} >
            <div className={"flex justify-left"}>
                <div className={"my-4 mx-4"}>
                    <Link to={"/"}
                          className="focus:outline-none text-white bg-green-700 hover:bg-green-800 focus:ring-4 focus:ring-green-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-green-600 dark:hover:bg-green-700 dark:focus:ring-green-800">
                        До хати
                    </Link>
                    {/*<Link to={"/create"} className={""}>Додати категорію</Link>*/}
                </div>
                <div className={"my-4 mx-4"}>
                    <Link to={"/create"}
                          className="focus:outline-none text-white bg-green-700 hover:bg-green-800 focus:ring-4 focus:ring-green-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-green-600 dark:hover:bg-green-700 dark:focus:ring-green-800">
                        Додати
                    </Link>
                    {/*<Link to={"/create"} className={""}>Додати категорію</Link>*/}
                </div>
            </div>
            <Routes>
                <Route path={"/"}>
                    <Route index element={<CategoriesListPage/>}/>
                    <Route path={"create"} element={<CategoryCreatePage/>}/>
                </Route>
            </Routes>
        </div>
    )
}

export default App
