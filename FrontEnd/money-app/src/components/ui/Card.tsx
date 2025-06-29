import { ReactNode } from "react";
import clsx from "clsx";
import { title } from "process";

interface CardProps {
   title?: string;
   className?: string;
   children: ReactNode;
}

export default function Card({
   title = "",
   className = "",
   children,
}: CardProps) {
   return (
      <section
         className={clsx(
            "bg-white rounded-lg shadow-lg p-6 flex flex-col",
            className
         )}
      >
         {title && (
            <div className="text-center">
               <h2 className="text-gray-700 text-xl">
                  <b>{title}</b>
               </h2>
            </div>
         )}
         {children}
      </section>
   );
}
