import type {ICategoryItem} from "../../../types/category/ICategoryItem.ts";

interface Props {
    category: ICategoryItem;
}

const CategoryRow : React.FC<Props> = ({
                                    category,
                                       }) => {

    return (
        <tr className="bg-white border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200">
            <td className="px-6 py-4">
                <img src={category.image} alt={category.name} width={75}/>
            </td>
            <th scope="row"
                className="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                {category.name}
            </th>
        </tr>
    )
}
export default CategoryRow;