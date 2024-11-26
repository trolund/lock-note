import { Header } from "./header";

import { ReactNode } from "react";

type LayoutProps = {
  children: ReactNode;
};

export const Layout = ({ children }: LayoutProps) => {
  return (
    <>
      <div className="fixed w-screen h-14 top-0 left-0 bg-slate-500">
        <Header />
      </div>

      {children}
    </>
  );
};
